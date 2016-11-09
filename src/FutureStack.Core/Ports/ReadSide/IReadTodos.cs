using System;
using System.Collections.Generic;
using FutureStack.Core.Model;

namespace FutureStack.Core.Ports.ReadSide
{
    public interface IReadTodos : IGetAllTodos, IGetTodos
    {
    }

    public class ReadTodosQueryDelegetor : IReadTodos
    {
        private readonly IGetAllTodos _getAllTodosQuery;
        private readonly IGetTodos _getTodosQuery;

        public ReadTodosQueryDelegetor(IGetAllTodos getAllTodosQuery, IGetTodos getTodosQuery)
        {
            _getAllTodosQuery = getAllTodosQuery;
            _getTodosQuery = getTodosQuery;
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            return _getAllTodosQuery.GetAllTodos();
        }

        public Todo GetTodo(Guid id)
        {
            return _getTodosQuery.GetTodo(id);
        }
    }
}