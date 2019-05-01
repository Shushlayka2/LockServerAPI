using LockServerAPI.Models.BaseDataAccesses;
using LockServerAPI.Models.User;
using LockServerAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LockServerAPI.Controllers
{
    public class UserAuthController : Controller
    {
        protected IDataAccessService DataAccessService { get; }
        protected AuthOptions AuthOptions { get; }

        public UserAuthController(IDataAccessService dataAccessService, AuthOptions authOptions)
        {
            DataAccessService = dataAccessService;
            AuthOptions = authOptions;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var isExists = false;
                using (var dataAccess = DataAccessService.GetDataAccess<IUserDataAccess>())
                {
                    isExists = dataAccess.FindUser(model.Username, model.Password);
                }

                if (isExists)
                {
                    var identity = GetIdentity(model.Username);
                    return SendToken(identity);
                }
                ModelState.AddModelError("", "Incorrect login or passwords");
            }
            return BadRequest(ModelState);
        }

        protected ClaimsIdentity GetIdentity(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return id;
        }

        protected IActionResult SendToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return Json(encodedJwt);
        }
    }
}