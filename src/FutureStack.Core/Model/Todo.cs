using System;

namespace FutureStack.Core.Model
{
    public class Todo
    {
        public Guid Id { get; }
        public string Title { get; }
        public bool Completed { get; }

        public Todo(Guid id, string title, bool completed = false)
        {
            Title = title;
            Completed = completed;
            Id = id;
        }
    }
}