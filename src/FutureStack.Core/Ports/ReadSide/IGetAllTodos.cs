using System.Collections;
using System.Collections.Generic;
using FutureStack.Core.Adaptors.Repositories;
using FutureStack.Core.Model;

namespace FutureStack.Core.Ports.ReadSide
{
    public interface IGetAllTodos
    {
        IEnumerable<Todo> GetAllTodos();
    }

    public class GetAllTodosQuery : IGetAllTodos
    {
        private readonly ITodoRepository _todoRepository;

        public GetAllTodosQuery(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            return _todoRepository.GetAllTodos();
        }
    }
}