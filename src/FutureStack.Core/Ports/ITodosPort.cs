using System;

namespace FutureStack.Core.Ports
{
    public interface ITodosPort : ICreateTodos
    {
        
    }

    public class TodosAppServiceDelegator : ITodosPort
    {
        private readonly ICreateTodos _createTodosService;

        public TodosAppServiceDelegator(ICreateTodos createTodosService)
        {
            _createTodosService = createTodosService;
        }

        public void CreateTodo(Guid id, string title)
        {
            _createTodosService.CreateTodo(id, title);
        }
    }
}
