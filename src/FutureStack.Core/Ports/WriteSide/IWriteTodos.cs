using System;

namespace FutureStack.Core.Ports.WriteSide
{
    public interface IWriteTodos : ICreateTodos, IDeleteTodos, IDeleteAllTodos
    {
        
    }

    public class WriteTodosAppServiceDelegator : IWriteTodos
    {
        private readonly ICreateTodos _createTodosService;
        private readonly IDeleteTodos _deleteTodosService;
        private readonly IDeleteAllTodos _deleteAllTodosService;

        public WriteTodosAppServiceDelegator(ICreateTodos createTodosService, IDeleteTodos deleteTodosService, IDeleteAllTodos deleteAllTodosService)
        {
            _createTodosService = createTodosService;
            _deleteTodosService = deleteTodosService;
            _deleteAllTodosService = deleteAllTodosService;
        }

        public void CreateTodo(Guid id, string title)
        {
            _createTodosService.CreateTodo(id, title);
        }

        public void DeleteTodo(Guid id)
        {
            _deleteTodosService.DeleteTodo(id);
        }

        public void DeleteAllTodos()
        {
            _deleteAllTodosService.DeleteAllTodos();
        }
    }
}
