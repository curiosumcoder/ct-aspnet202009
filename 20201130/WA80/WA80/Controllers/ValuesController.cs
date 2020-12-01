using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WA80.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/Values        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //[Microsoft.AspNetCore.Mvc.AcceptVerbs("LINK")]
        [HttpGet("Get2")]
        public IEnumerable<string> Get2()
        {
            return new string[] { "Get2 - value1", "Get2 - value2" };
        }

        // GET api/Values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Values
        [HttpPost]
        //public void Post([FromBody] string value)
        public void Post([FromForm] string value)
        {

        }

        // PUT api/Values/5
        [HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        public void Put(int id, [FromForm] string value)
        {
        }

        // DELETE api/Values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
