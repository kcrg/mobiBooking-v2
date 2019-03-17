using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.Models;
using mobiBooking.Model.Room.Request;
using mobiBooking.Model.SendModels;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobiBooking.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService, IAccountService authenticateService)
        {
            _roomService = roomService;
        }

        // GET api/values
        [Authorize(Roles = "Administrator, User")]
        [HttpGet("get_all")]
        public async Task<ActionResult<IEnumerable<RoomDataModel>>> Get()
        {
            return Ok(await _roomService.GetAll());
        }

        [Authorize(Roles = "Administrator, User")]
        [HttpPost("for_reservation")]
        public async Task<ActionResult<IEnumerable<RoomDataModel>>> GetForReservationn([FromBody] RoomsForReservationModel roomsForReservationModel)
        {
            return Ok(await _roomService.GetForReservation(roomsForReservationModel));
        }

        // GET api/values/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<RoomModel>> Get(int id)
        { 
            RoomModel roomModel = await _roomService.Get(id);

            if (roomModel == null)
            {
                return BadRequest(new { message = "Room with this id is not exsist." });
            }

            return Ok(roomModel);
        }

        // POST api/values
        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> Post([FromBody]RoomModel roomParam)
        {
            if (await _roomService.Create(roomParam))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Value can not be null" });
            }
        }

        // PUT api/values/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] RoomModel value)
        {           
            await _roomService.Update(id, value);

            return Ok();
        }

        // DELETE api/values/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            
            if (await _roomService.Delete(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Room with this id is not exsist." });
            }
        }
    }
}
