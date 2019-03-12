﻿using System.Collections.Generic;
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
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDataModel>> GetAll()
        {
            if (!IsLoggedIn(out ActionResult result))
                return result;

            return Ok(_usersService.GetAll());
        }

        // POST: api/Users
        //[AllowAnonymous]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Post([FromBody] CreateUserModel value)
        {
            if (!IsLoggedIn(out ActionResult result))
                return result;

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
            if (!IsLoggedIn(out ActionResult result))
                return result;

            _usersService.Update(id, value);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!IsLoggedIn(out ActionResult result))
                return result;

            _usersService.Delete(id);
            return Ok();
        }
    }
}
