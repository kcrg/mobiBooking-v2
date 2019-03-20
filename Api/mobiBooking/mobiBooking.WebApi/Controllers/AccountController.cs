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
        public async Task<ActionResult<TokenModel>> LoginAsync([FromBody]AuthenticateUserModel userParam)
        {
            TokenModel tokenModel = await _accountService.AuthenticateAsync(userParam.Email, userParam.Password);
            if (tokenModel != null)
            {
                return Ok(tokenModel);
            }
            else
            {
                return BadRequest(new { message = "Incorrect email or password" });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateUserAsync([FromBody] CreateUserModel value)
        {

            if (await _accountService.CreateAsync(value))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Bad data or user with this email or username already exists." });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateUserAsync(int id, [FromBody] EditUserModel value)
        {
            if (await _accountService.UpdateAsync(id, value))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Bad data or user with this email or username already exists." });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _accountService.DeleteAsync(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "User with this id is not exists." });
            }
        }
    }
}
