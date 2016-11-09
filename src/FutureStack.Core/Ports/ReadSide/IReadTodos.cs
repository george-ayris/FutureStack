using System.Collections.Generic;
using FutureStack.Core.Model;

namespace FutureStack.Core.Ports.ReadSide
{
    public interface IReadTodos : IGetAllTodos
    {
        
    }

    public class ReadTodosQueryDelegetor : IReadTodos
    {
        private readonly IGetAllTodos _getAllTodosQuery;

        public ReadTodosQueryDelegetor(IGetAllTodos getAllTodosQuery)
        {
            _getAllTodosQuery = getAllTodosQuery;
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            return _getAllTodosQuery.GetAllTodos();
        }
    }
}