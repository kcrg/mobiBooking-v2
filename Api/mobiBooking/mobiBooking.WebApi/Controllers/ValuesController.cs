using Microsoft.AspNetCore.Mvc;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace mobiBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;

        public ValuesController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Room>> Get()
        {


            return Ok(_repoWrapper.Room.FindAll());     // db.Rooms.ToList();
               // return new string[] { "value1", "value2" };
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
