using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.Meetings.Response;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<string>> GetMeetingsThisWeek()
        {
            return Ok(await _meetingsService.GetMeetingsThisWeek(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("last_week")]
        public async Task<ActionResult<string>> GetMeetingsLastWeek()
        {
            return Ok(await _meetingsService.GetMeetingsLastWeek(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("this_month")]
        public async Task<ActionResult<string>> GetMeetingsThisMonth()
        {
            return Ok(await _meetingsService.GetMeetingsThisMonth(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("last_month")]
        public async Task<ActionResult<string>> GetMeetingsLastMonth()
        {
            return Ok(await _meetingsService.GetMeetingsLastMonth(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("count_this_week")]
        public async Task<ActionResult<int>> GetMeetingsCountThisWeek()
        {
            return Ok(await _meetingsService.GetMeetingsCountThisWeek(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("count_last_week")]
        public async Task<ActionResult<int>> GetMeetingsCountLastWeek()
        {
            return Ok(await _meetingsService.GetMeetingsCountLastWeek(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("count_this_month")]
        public async Task<ActionResult<int>> GetMeetingsCountThisMonth()
        {
            return Ok(await _meetingsService.GetMeetingsCountThisMonth(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("count_last_month")]
        public async Task<ActionResult<int>> GetMeetingsCountLastMonth()
        {
            return Ok(await _meetingsService.GetMeetingsCountLastMonth(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("today")]
        public async Task<ActionResult<IEnumerable<MeetingModel>>> GetMeetingsToday()
        {
            return Ok(await _meetingsService.GetMeetingsToday(int.Parse(User.Identity.Name)));
        }

        [Authorize(Roles = "Administrator,User")]
        [HttpGet("last_reservations")]
        public async Task<ActionResult<IEnumerable<LastReservationModel>>> GetLastReservations()
        {
            return Ok(await _meetingsService.GetLastReservations(int.Parse(User.Identity.Name)));
        }
    }
}
