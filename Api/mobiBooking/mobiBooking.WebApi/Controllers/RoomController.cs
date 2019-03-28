using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Data.Model;
using mobiBooking.Model.Models;
using mobiBooking.Model.Room.Request;
using mobiBooking.Model.Room.Response;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mobiBooking.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [Authorize(Roles = "Administrator, User")]
        [HttpGet("get_all")]
        public async Task<ActionResult<IEnumerable<RoomDataModel>>> GetAllAsync(bool orderByName)
        {
            return Ok(await _roomService.GetAllAsync(orderByName));
        }

        [Authorize(Roles = "Administrator, User")]
        [HttpGet("get_reservated")]
        public async Task<ActionResult<int>> GetReservatedNumberAsync()
        {
            return Ok(await _roomService.GetReservatedNumberAsync());
        }

        [Authorize(Roles = "Administrator, User")]
        [HttpGet("get_not_reservated")]
        public async Task<ActionResult<int>> GetNotReservatedNumberAsync()
        {
            return Ok(await _roomService.GetNotReservatedNumberAsync());
        }

        [Authorize(Roles = "Administrator, User")]
        [HttpGet("get_room_availabilities")]
        public async Task<ActionResult<IEnumerable<RoomAvailability>>> GetRoomAvailabilitiesAsync()
        {
            return Ok(await _roomService.GetRoomAvailabilitiesAsync());
        }

        [Authorize(Roles = "Administrator, User")]
        [HttpPost("for_reservation")]
        public async Task<ActionResult<IEnumerable<RoomDataModelForReservation>>> GetForReservationnAsync([FromBody] RoomsForReservationModel roomsForReservationModel)
        {
            return Ok(await _roomService.GetForReservationAsync(roomsForReservationModel));
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<RoomDataModel>> GetAsync(int id)
        {
            RoomDataModel roomModel = await _roomService.GetAsync(id);

            if (roomModel == null)
            {
                return BadRequest(new { message = "Room with this id is not exsist." });
            }

            return Ok(roomModel);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost("create")]
        public async Task<ActionResult> PostAsync([FromBody]RoomModel roomParam)
        {
            if (await _roomService.CreateAsync(roomParam))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Room with this name already exsist or incorrect availability" });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] RoomModel value)
        {
            if (await _roomService.UpdateAsync(id, value))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Room with this name already exsist or incorrect availability" });
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            if (await _roomService.DeleteAsync(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new { message = "Room with this id is not exsist or has reservation." });
            }
        }
    }
}
