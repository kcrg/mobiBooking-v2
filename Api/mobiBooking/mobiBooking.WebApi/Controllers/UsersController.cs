using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Service.Interfaces;

namespace mobiBooking.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUsersService _usersService;
        public UsersController(IUsersService usersService, IAuthenticateService authenticateService)
        {
            _usersService = usersService;
        }

        // GET: api/Users
        [Authorize(Roles = "Administrator, User")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDataModel>> GetAll()
        {
            return Ok(_usersService.GetAll());
        }

        // POST: api/Users
        //[AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Post([FromBody] CreateUserModel value)
        {

            if (_usersService.Create(value))
                return Ok();
            else
                return BadRequest(new { message = "Bad data or user with this email or username already exists." });

        }

        // PUT: api/Users/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CreateUserModel value)
        {

            _usersService.Update(id, value);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _usersService.Delete(id);
            return Ok();
        }
    }
}
