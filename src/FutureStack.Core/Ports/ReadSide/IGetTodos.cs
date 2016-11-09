using System;
using FutureStack.Core.Adaptors.Repositories;
using FutureStack.Core.Model;

namespace FutureStack.Core.Ports.ReadSide
{
    public interface IGetTodos
    {
        Todo GetTodo(Guid id);
    }

    public class GetTodoQuery : IGetTodos
    {
        private readonly ITodoRepository _todoRepository;

        public GetTodoQuery(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Todo GetTodo(Guid id)
        {
            return _todoRepository.GetTodo(id);
        }
    }
}