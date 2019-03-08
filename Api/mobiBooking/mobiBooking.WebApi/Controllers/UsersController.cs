using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.SendModels;
using mobiBooking.Service.Interfaces;
using mobiBooking.WebApi.Base;

namespace mobiBooking.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class UsersController : ApiControllerBase
    {
        private IUsersService _usersService;
        public UsersController(IUsersService usersService, IAuthenticateService authenticateService) : base(authenticateService)
        {
            _usersService = usersService;
        }

        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<UserDataModel>> GetAll()
        {
            if (!HasPermission(GET_USERS, out ActionResult result))
                return result;

            return Ok(_usersService.GetAll());
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult Post([FromBody] CreateUserModel value)
        {
            if (!HasPermission(ADD_USER, out ActionResult result))
                return result;

            _usersService.Create(value);
            return Ok();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] CreateUserModel value)
        {
            if (!HasPermission(UPDATE_USER, out ActionResult result))
                return result;

            _usersService.Update(id, value);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!HasPermission(DELETE_USER, out ActionResult result))
                return result;

            _usersService.Delete(id);
            return Ok();
        }
    }
}
