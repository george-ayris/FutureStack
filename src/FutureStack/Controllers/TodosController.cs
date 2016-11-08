using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FutureStack.Views;
using Microsoft.AspNetCore.Mvc;

namespace FutureStack.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        // GET api/todos
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[0];
        }

        // GET api/todos/5
        [HttpGet("{id}", Name = "GetTodo")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/todos
        [HttpPost]
        public IActionResult Post([FromBody]TodoView todo)
        {
            return new CreatedAtRouteResult("GetTodo", new { id = 1 }, todo);
        }

        // PUT api/todos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string todo)
        {
        }

        // DELETE api/todos/
        [HttpDelete]
        public void DeleteAll(int id)
        {
        }

        // DELETE api/todos/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
