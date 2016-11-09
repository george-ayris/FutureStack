using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FutureStack.Api.Views;
using FutureStack.Core.Ports.WriteSide;
using FutureStack.Core.Ports.ReadSide;
using System.Linq;

namespace FutureStack.Api.Controllers
{
    [Route("api/[controller]")]
    public class TodosController : Controller
    {
        private readonly IWriteTodos _todosWriter;
        private readonly IReadTodos _todosReader;

        public TodosController(IWriteTodos todosWriter, IReadTodos todosReader)
        {
            _todosWriter = todosWriter;
            _todosReader = todosReader;
        }

        // GET api/todos
        [HttpGet]
        public IEnumerable<TodoView> Get()
        {
            var todos = _todosReader.GetAllTodos();
            return todos.Select(t => new TodoView {Title = t.Title});

        }

        // GET api/todos/5
        [HttpGet("{id}", Name = "GetTodo")]
        public TodoView Get(Guid id)
        {
            var todo = _todosReader.GetTodo(id);
            return new TodoView { Title = todo.Title };
        }

        // POST api/todos
        [HttpPost]
        public IActionResult Post([FromBody]TodoView todo)
        {
            var id = Guid.NewGuid();
            _todosWriter.CreateTodo(id, todo.Title);
            return new CreatedAtRouteResult("GetTodo", new { id }, todo);
        }

        // PUT api/todos/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string todo)
        {
        }

        // DELETE api/todos/
        [HttpDelete]
        public IActionResult DeleteAll()
        {
            _todosWriter.DeleteAllTodos();
            return NoContent();
        }

        // DELETE api/todos/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _todosWriter.DeleteTodo(id);
            return NoContent();
        }
    }
}
