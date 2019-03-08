using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public ApiControllerBase(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        protected bool IsLogin(out ActionResult actionResult)
        {
            actionResult = BadRequest(new { message = "You are logout" });

            return _authenticateService.IsLogin(int.Parse(User.Identity.Name));
        }
    }
}
