using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace mobiBooking.WebApi.Base
{
    [Authorize]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public ApiControllerBase(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        //Permissions
        protected static readonly string ADD_USERS = "ADD_USERS";

        protected bool HasPermission(string permission, out ActionResult actionResult)
        {
            actionResult = BadRequest(new { message = "Permission needed: " + permission });

            return _authenticateService.HasPermission(int.Parse(User.Identity.Name), permission);
        }
    }
}
