using System;

namespace TodoAPI.Contexts.Todos.Commands
{
    public class CreateTodoCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}