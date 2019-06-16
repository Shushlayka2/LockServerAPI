using LockServerAPI.Models.BaseDataAccesses;
using LockServerAPI.Models.Code;
using LockServerAPI.ViewModels;
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

        [HttpPost]
        [Route("generatecode")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public void GenerateCode([FromBody]CodeViewModel model)
        {
            using (var dataAccess = DataAccessService.GetDataAccess<ICodeDataAccess>())
            {
                dataAccess.GenerateCode(model.LockId, model.Config);
            }
        }

        [HttpPost]
        [Route("editcode")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public void EditCode([FromBody]CodeViewModel model)
        {
            using (var dataAccess = DataAccessService.GetDataAccess<ICodeDataAccess>())
            {
                dataAccess.EditCode(model);
            }
        }

        [HttpPost]
        [Route("removecode")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public void RemoveCode([FromBody]Code code)
        {
            using (var dataAccess = DataAccessService.GetDataAccess<ICodeDataAccess>())
            {
                dataAccess.RemoveCode(code);
            }
        }
    }
}