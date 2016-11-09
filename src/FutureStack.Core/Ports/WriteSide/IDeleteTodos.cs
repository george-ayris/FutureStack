using System;
using FutureStack.Core.Adaptors.Repositories;

namespace FutureStack.Core.Ports.WriteSide
{
    public interface IDeleteTodos
    {
        void DeleteTodo(Guid id);
    }

    public class DeleteTodosAppService : IDeleteTodos
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteTodosAppService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public void DeleteTodo(Guid id)
        {
            _todoRepository.DeleteTodo(id);
        }
    }
}