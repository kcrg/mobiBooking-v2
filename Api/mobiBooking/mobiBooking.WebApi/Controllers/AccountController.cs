using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.Models;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Model.User.Request;
using mobiBooking.Service.Interfaces;
using System.Threading.Tasks;

namespace mobiBooking.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenModel>> Login([FromBody]AuthenticateUserModel userParam)
        {
            TokenModel user = await _accountService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // POST: api/Users
        //[AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserModel value)
        {

            if (await _accountService.Create(value))
                return Ok();
            else
                return BadRequest(new { message = "Bad data or user with this email or username already exists." });

        }

        // PUT: api/Users/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] EditUserModel value)
        {
            if (await _accountService.Update(id, value))
                return Ok();
            else
                return BadRequest(new { message = "Bad data or user exists." });
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _accountService.Delete(id);
            return Ok();
        }
    }
}
