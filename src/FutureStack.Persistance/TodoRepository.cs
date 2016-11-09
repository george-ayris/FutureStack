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

        public Todo GetTodo(Guid id)
        {
            return _savedTodos[id];
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            return _savedTodos.Values;
        }

        public void DeleteTodo(Guid id)
        {
            _savedTodos.Remove(id);
        }

        public void DeleteAllTodos()
        {
            _savedTodos.Clear();
        }
    }
}
