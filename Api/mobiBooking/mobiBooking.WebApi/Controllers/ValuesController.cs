using Microsoft.AspNetCore.Mvc;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace mobiBooking.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Room>> Get()
        {

            using (MobiBookingDBContext db = new MobiBookingDBContext())
            {
                return db.Rooms.ToList();

               // return new string[] { "value1", "value2" };
            }
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
