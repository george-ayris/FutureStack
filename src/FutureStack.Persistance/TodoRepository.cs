using System;
using System.Collections.Generic;
using FutureStack.Core.Adaptors.Repositories;
using FutureStack.Core.Model;

namespace FutureStack.Persistance
{
    public class TodoRepository : ITodoRepository
    {
        private readonly Dictionary<Guid, Todo> _savedTodos;
        public TodoRepository()
        {
            _savedTodos = new Dictionary<Guid, Todo>();
        }

        public void SaveTodo(Todo todo)
        {
            _savedTodos.Add(todo.Id, todo);
        }
    }
}
