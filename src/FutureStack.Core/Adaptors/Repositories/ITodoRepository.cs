using System.Collections.Generic;
using FutureStack.Core.Model;

namespace FutureStack.Core.Adaptors.Repositories
{
    public interface ITodoRepository
    {
        void SaveTodo(Todo todo);
        IEnumerable<Todo> GetAllTodos();
    }
}