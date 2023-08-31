using ERS_FE_.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ERS_FE_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Enrollment : ControllerBase
    {
        private readonly ISupabaseService _supabaseService;
        public Enrollment(ISupabaseService supabase) 
        { 
            _supabaseService = supabase;
        }

        // GET: api/<Enrollment>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Enrollment>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Enrollment>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Enrollment>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Enrollment>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
