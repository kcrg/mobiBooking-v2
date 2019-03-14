using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Service.Interfaces;


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

        // GET: api/Reservation
        [HttpGet("get_all")]
        public ActionResult<IEnumerable<ReservationModel>> Get()
        {
            return Ok();
        }

        // GET: api/Reservation/5
        [HttpGet("get/{id}", Name = "Get")]
        public ActionResult Get(int id)
        { 
            return Ok();
        }

        // POST: api/Reservation
        [HttpPost("create")]
        public async Task<ActionResult> Post([FromBody] ReservationModel value)
        {
            if (await _reservationService.Create(value, int.Parse(User.Identity.Name)))
                return Ok();
            else
                return BadRequest(new { message = "Wrong data" });
        }

        // PUT: api/Reservation/5
        [HttpPut("update/{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("delete/{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
