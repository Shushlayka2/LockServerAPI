using LockServerAPI.Models.BaseDataAccesses;
using LockServerAPI.Models.Code;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LockServerAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        protected IDataAccessService DataAccessService { get; }

        public CodeController(IDataAccessService dataAccessService)
        {
            DataAccessService = dataAccessService;
        }

        [HttpGet]
        [Route("getcodes")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GetCodes()
        {
            List<Code> result = null;
            using (var dataAccess = DataAccessService.GetDataAccess<ICodeDataAccess>())
            {
                result = dataAccess.GetCodes();
            }
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("generatecode")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult GenerateCode()
        {
            List<Code> result = null;
            using (var dataAccess = DataAccessService.GetDataAccess<ICodeDataAccess>())
            {
                dataAccess.GenerateCode();
                result = dataAccess.GetCodes();
            }
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("removecode")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult RemoveCode([FromBody]Code code)
        {
            List<Code> result = null;
            using (var dataAccess = DataAccessService.GetDataAccess<ICodeDataAccess>())
            {
                dataAccess.RemoveCode(code);
                result = dataAccess.GetCodes();
            }
            return new JsonResult(result);
        }
    }
}