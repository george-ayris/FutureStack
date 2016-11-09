using System;

namespace FutureStack.Core.Model
{
    public class Todo
    {
        public Guid Id { get; }
        public string Title { get; }

        public Todo(Guid id, string title)
        {
            Title = title;
            Id = id;
        }
    }
}