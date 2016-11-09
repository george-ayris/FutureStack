using System;
using FutureStack.Core.Adaptors.Repositories;
using FutureStack.Core.Model;

namespace FutureStack.Core.Ports.WriteSide
{
    public interface ICreateTodos
    {
        Todo CreateTodo(Guid id, string title);
    }

    public class CreateTodosAppService : ICreateTodos
    {
        private readonly ITodoRepository _todoRepository;

        public CreateTodosAppService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Todo CreateTodo(Guid id, string title)
        {
            var todo = new Todo(id, title);
            _todoRepository.SaveTodo(todo);
            return todo;
        }
    }
}