using LockServerAPI.Models.BaseDataAccesses;
using LockServerAPI.Models.DataAccesses;
using Microsoft.AspNetCore.Mvc;

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
            string user_id;
            using (var dataAccess = DataAccessService.GetDataAccess<ICodesDataAccess>())
            {
                user_id = dataAccess.FindCode(value);
            }

            if (user_id != null)
            {
                return new JsonResult(user_id);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }
    }
}