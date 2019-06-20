using LockServerAPI.Models.BaseDataAccesses;
using LockServerAPI.Models.Code;
using LockServerAPI.Models.Lock;
using LockServerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LockServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistryController : ControllerBase
    {
        protected IDataAccessService DataAccessService { get; }

        public RegistryController(IDataAccessService dataAccessService)
        {
            DataAccessService = dataAccessService;
        }

        [HttpGet]
        public string Get()
        {
            return "test";
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDevice([FromBody] string codeVal)
        {
            try
            {
                //Find the lock
                (string lockId, string config) tuple = (null, null);
                using (var dataAccess = DataAccessService.GetDataAccess<ICodeDataAccess>())
                {
                    tuple = dataAccess.FindCode(codeVal);
                }

                //If lock doesn't find, send unautorized code
                if (tuple.lockId == null)
                {
                    return new UnauthorizedResult();
                }

                //Generate and save device identifier
                string deviceId;
                using (var dataAccess = DataAccessService.GetDataAccess<ILockDataAccess>())
                {
                    deviceId = dataAccess.RegisterLock(tuple.lockId, tuple.config);
                }

                //Transfer deviceId to lock controller
                var _IoTServiceProxy = new IoTServiceProxy(tuple.lockId, tuple.config);
                var isRegistered = await _IoTServiceProxy.RegisterDevice(deviceId);

                if (isRegistered)
                {
                    using (var dataAccess = DataAccessService.GetDataAccess<ICodeDataAccess>())
                    {
                        dataAccess.RemoveCode(new Code
                        {
                            LockId = tuple.lockId,
                            CodeVal = codeVal
                        });
                    }
                    return new JsonResult((deviceId, tuple.config));
                }
                else
                {
                    return StatusCode(500);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}