using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("authenticate")]
        public ActionResult<UserDataModel> Authenticate([FromBody]AuthenticateUserModel userParam)
        {
            var user = _authenticateService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("LogOut/{id}")]
        public ActionResult<UserDataModel> LogOut(int id)
        {
            _authenticateService.LogOut(id);

            return Ok();
        }
    }
}
