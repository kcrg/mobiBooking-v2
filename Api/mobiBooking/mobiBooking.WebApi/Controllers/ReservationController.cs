using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.RecivedModels;
using mobiBooking.Service.Interfaces;
using mobiBooking.WebApi.Base;

namespace mobiBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : ApiControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IAuthenticateService authenticateService, IReservationService reservationService) : base(authenticateService)
        {
            _reservationService = reservationService;
        }

        // GET: api/Reservation
        [HttpGet]
        public ActionResult<IEnumerable<ReservationModel>> Get()
        {
            if (!IsLoggedIn(out ActionResult result))
                return result;

            return Ok();
        }

        // GET: api/Reservation/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            if (!IsLoggedIn(out ActionResult result))
                return result;

            return Ok();
        }

        // POST: api/Reservation
        [HttpPost]
        public ActionResult Post([FromBody] ReservationModel value)
        {
            if (!IsLoggedIn(out ActionResult result))
                return result;

            if (_reservationService.Create(value, int.Parse(User.Identity.Name)))
                return Ok();
            else
                return BadRequest(new { message = "Wrong data" });
        }

        // PUT: api/Reservation/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (!IsLoggedIn(out ActionResult result))
                return result;

            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!IsLoggedIn(out ActionResult result))
                return result;

            return Ok();
        }
    }
}
