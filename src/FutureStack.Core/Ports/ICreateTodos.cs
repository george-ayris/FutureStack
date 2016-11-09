using System;
using FutureStack.Core.Adaptors.Repositories;
using FutureStack.Core.Model;

namespace FutureStack.Core.Ports
{
    public interface ICreateTodos
    {
        void CreateTodo(Guid id, string title);
    }

    public class CreateTodosAppService : ICreateTodos
    {
        private readonly ITodoRepository _todoRepository;

        public CreateTodosAppService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public void CreateTodo(Guid id, string title)
        {
            _todoRepository.SaveTodo(new Todo(id, title));
        }
    }
}