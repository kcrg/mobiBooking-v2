using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using mobiBooking.Data;
using mobiBooking.Data.Model;
using mobiBooking.Repository.Base;
using System.Collections.Generic;
using System.Linq;

namespace mobiBooking.WebApi.Controllers
{
    [EnableCors("allowAll")]
    [Authorize]
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


            return Ok(_repoWrapper.Room.FindAll());   
            
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok(_repoWrapper.Room.Find(id)); 
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Room roomParam)
        {
            _repoWrapper.Room.Create(roomParam);
            _repoWrapper.Room.Save();
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
