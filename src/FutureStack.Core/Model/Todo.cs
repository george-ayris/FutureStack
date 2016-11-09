using System;

namespace FutureStack.Core.Model
{
    public class Todo
    {
        public Guid Id { get; }
        public string Title { get; }
        public string Completed { get; }

        public Todo(Guid id, string title)
        {
            Title = title;
            Completed = "NotCompleted";
            Id = id;
        }
    }
}