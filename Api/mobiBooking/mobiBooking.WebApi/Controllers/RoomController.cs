using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Model.Models;
using mobiBooking.Model.SendModels;
using mobiBooking.Service.Interfaces;
using System.Collections.Generic;

namespace mobiBooking.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService, IAuthenticateService authenticateService)
        {
            _roomService = roomService;
        }

        // GET api/values
        [Authorize(Roles = "Administrator, User")]
        [HttpGet]
        public ActionResult<IEnumerable<RoomDataModel>> Get()
        {
            return Ok(_roomService.GetAll());
        }

        // GET api/values/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}")]
        public ActionResult<RoomModel> Get(int id)
        { 
            RoomModel roomModel = _roomService.Get(id);

            if (roomModel == null)
            {
                return BadRequest(new { message = "Room with this id is not exsist." });
            }

            return Ok(roomModel);
        }

        // POST api/values
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult Post([FromBody]RoomModel roomParam)
        {
            

            if (_roomService.Create(roomParam))
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
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] RoomModel value)
        {
            

            _roomService.Update(id, value);

            return Ok();
        }

        // DELETE api/values/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            

            if (_roomService.Delete(id))
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
