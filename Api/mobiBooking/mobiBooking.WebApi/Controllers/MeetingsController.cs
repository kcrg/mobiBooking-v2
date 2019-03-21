using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Service.Interfaces;

namespace mobiBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingsService _meetingsService;
        public MeetingsController(IMeetingsService meetingsService)
        {
            _meetingsService = meetingsService;
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("this_week")]
        public ActionResult<int> GetMeetingsThisWeek()
        {
            return Ok(_meetingsService.GetMeetingsThisWeek(int.Parse(User.Identity.Name)));
        }
    }
}
