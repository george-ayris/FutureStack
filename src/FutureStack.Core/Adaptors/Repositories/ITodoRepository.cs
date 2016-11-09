using System;
using System.Collections.Generic;
using FutureStack.Core.Model;

namespace FutureStack.Core.Adaptors.Repositories
{
    public interface ITodoRepository
    {
        void SaveTodo(Todo todo);
        Todo GetTodo(Guid id);
        IEnumerable<Todo> GetAllTodos();
        void DeleteTodo(Guid id);
        void DeleteAllTodos();
    }
}