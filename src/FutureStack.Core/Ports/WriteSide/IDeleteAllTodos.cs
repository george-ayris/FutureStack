using FutureStack.Core.Adaptors.Repositories;

namespace FutureStack.Core.Ports.WriteSide
{
    public interface IDeleteAllTodos
    {
        void DeleteAllTodos();
    }

    public class DeleteAllTodosAppService : IDeleteAllTodos
    {
        private readonly ITodoRepository _todoRepository;

        public DeleteAllTodosAppService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public void DeleteAllTodos()
        {
            _todoRepository.DeleteAllTodos();
        }
    }
}