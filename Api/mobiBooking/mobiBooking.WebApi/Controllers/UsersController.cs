using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.SendModels;
using mobiBooking.Service.Interfaces;

namespace mobiBooking.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;
        public UsersController(IUsersService usersService, IAccountService authenticateService)
        {
            _usersService = usersService;
        }

        // GET: api/Users
        [Authorize(Roles = "Administrator, User")]
        [HttpGet("get_all")]
        public async Task<ActionResult<IEnumerable<UserDataModel>>> GetAllAsync()
        {
            return Ok(await _usersService.GetAllAsync(User.IsInRole("Administrator")));
        }
    }
}
