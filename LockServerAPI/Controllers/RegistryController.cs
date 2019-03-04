using LockServerAPI.Models.BaseDataAccesses;
using LockServerAPI.Models.Codes;
using LockServerAPI.Models.Locks;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpPost]
        public IActionResult IsCodeValid([FromBody] string value)
        {
            try
            {
                string lockId;
                using (var dataAccess = DataAccessService.GetDataAccess<ICodesDataAccess>())
                {
                    lockId = dataAccess.FindCode(value);
                }

                if (lockId == null)
                {
                    return new UnauthorizedResult();
                }

                string deviceId;
                using (var dataAccess = DataAccessService.GetDataAccess<ILocksDataAccess>())
                {
                    deviceId = dataAccess.RegisterLock(lockId);
                }

                //TODO: Transfer deviceId to OrangePI controller

                return new JsonResult(deviceId);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}