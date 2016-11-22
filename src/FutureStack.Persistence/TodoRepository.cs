using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using FutureStack.Core.Adaptors;
using FutureStack.Core.Adaptors.Repositories;
using FutureStack.Core.Model;

namespace FutureStack.Persistence
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IConfig _config;
        private readonly Dictionary<Guid, Todo> _savedTodos;
        public TodoRepository(IConfig config)
        {
            _config = config;
            _savedTodos = new Dictionary<Guid, Todo>();
        }

        public void SaveTodo(Todo todo)
        {
            _savedTodos.Add(todo.Id, todo);
        }

        public Todo GetTodo(Guid id)
        {
            return _savedTodos[id];
        }

        public IEnumerable<Todo> GetAllTodos()
        {
            using(var conn = new SqlConnection(_config.ConnectionString))
            {
                conn.Open();

                return conn.Query<Todo>(@"
                    SELECT TodoId AS Id, Title, Completed
                    FROM [dbo].[Todo]");
            }
        }

        public void DeleteTodo(Guid id)
        {
            _savedTodos.Remove(id);
        }

        public void DeleteAllTodos()
        {
            _savedTodos.Clear();
        }
    }
}
