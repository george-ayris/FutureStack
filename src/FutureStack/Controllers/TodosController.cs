using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FutureStack.Api.Views;
using FutureStack.Core.Ports;

namespace FutureStack.Api.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private readonly ITodosPort _todosPort;

        public TodosController(ITodosPort todosPort)
        {
            _todosPort = todosPort;
        }

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
            var id = Guid.NewGuid();
            _todosPort.CreateTodo(id, todo.Title);
            return new CreatedAtRouteResult("GetTodo", new { id }, todo);
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
