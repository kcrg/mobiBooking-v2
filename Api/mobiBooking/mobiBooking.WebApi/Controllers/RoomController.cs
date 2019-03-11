using Microsoft.AspNetCore.Mvc;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using mobiBooking.Service.Interfaces;
using mobiBooking.WebApi.Base;
using System.Collections.Generic;

namespace mobiBooking.WebApi.Controllers
{

    [Route("api/[controller]")]
    public class RoomController : ApiControllerBase
    {
        private readonly IRepositoryWrapper _repoWrapper;

        public RoomController(IRepositoryWrapper repoWrapper, IAuthenticateService authenticateService) : base(authenticateService)
        {
            _repoWrapper = repoWrapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Room>> Get()
        {
            if (!IsLogin(out ActionResult result))
            {
                return result;
            }

            return Ok(_repoWrapper.Room.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (!IsLogin(out ActionResult result))
            {
                return result;
            }

            return Ok(_repoWrapper.Room.Find(id));
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody]Room roomParam)
        {
            if (!IsLogin(out ActionResult result))
            {
                return result;
            }

            _repoWrapper.Room.Create(roomParam);
            _repoWrapper.Room.Save();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (!IsLogin(out ActionResult result))
            {
                return result;
            }

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!IsLogin(out ActionResult result))
            {
                return result;
            }

            return Ok();
        }
    }
}
