using System;

namespace FutureStack.Core.Ports.WriteSide
{
    public interface IWriteTodos : ICreateTodos
    {
        
    }

    public class WriteTodosAppServiceDelegator : IWriteTodos
    {
        private readonly ICreateTodos _createTodosService;

        public WriteTodosAppServiceDelegator(ICreateTodos createTodosService)
        {
            _createTodosService = createTodosService;
        }

        public void CreateTodo(Guid id, string title)
        {
            _createTodosService.CreateTodo(id, title);
        }
    }
}
