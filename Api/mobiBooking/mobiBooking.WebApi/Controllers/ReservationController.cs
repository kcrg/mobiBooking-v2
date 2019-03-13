using System.Collections.Generic;
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
        [HttpGet]
        public ActionResult<IEnumerable<ReservationModel>> Get()
        {
            return Ok();
        }

        // GET: api/Reservation/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        { 
            return Ok();
        }

        // POST: api/Reservation
        [HttpPost]
        public ActionResult Post([FromBody] ReservationModel value)
        {
            if (_reservationService.Create(value, int.Parse(User.Identity.Name)))
                return Ok();
            else
                return BadRequest(new { message = "Wrong data" });
        }

        // PUT: api/Reservation/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
