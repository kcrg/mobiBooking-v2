using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.Models;
using mobiBooking.Model.SendModels;
using mobiBooking.Service.Interfaces;
using mobiBooking.WebApi.Base;

namespace mobiBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticateController : ApiControllerBase
    {

        private readonly IAuthenticateService _authenticateService;
        public AuthenticateController(IAuthenticateService authenticateService) : base(authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult<UserDataModel> Login([FromBody]AuthenticateUserModel userParam)
        {
            var user = _authenticateService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("Logout")]
        public ActionResult Logout()
        {
            
            _authenticateService.Logout(int.Parse(User.Identity.Name));

            return Ok();
        }
    }
}
