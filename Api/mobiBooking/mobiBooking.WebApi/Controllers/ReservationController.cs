using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Model.Reservation.Response;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace mobiBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("get_all")]
        public ActionResult<IEnumerable<ReservationModel>> GetAsync()
        {
            return Ok();
        }

        [HttpGet("get/{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            return Ok();
        }

        [HttpGet("get_reservation_intervals")]
        public async Task<ActionResult<IEnumerable<ReservationIntervalModel>>> GetReservationIntervalsAsync()
        {
            return Ok(await _reservationService.GetReservationIntervalsAsync());
        }

        [HttpPost("create")]
        public async Task<ActionResult> PostAsync([FromBody] ReservationModel value)
        {
            if (await _reservationService.CreateAsync(value, int.Parse(User.Identity.Name)))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Wrong data or room reserved." });
            }
        }

        [HttpPut("update/{id}")]
        public ActionResult PutAsync(int id, [FromBody] string value)
        {
            return Ok();
        }

        [HttpDelete("delete/{id}")]
        public ActionResult DeleteAsync(int id)
        {
            return Ok();
        }
    }
}
