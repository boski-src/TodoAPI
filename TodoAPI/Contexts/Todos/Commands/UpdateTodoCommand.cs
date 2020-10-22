using System;

namespace TodoAPI.Contexts.Todos.Commands
{
    public class UpdateTodoCommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}