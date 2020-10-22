using System;
using TodoAPI.Types;

namespace TodoAPI.Contexts.Todos
{
    public class Todo : IEntity
    {
        public  Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CompletePercentage { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public Todo(
            Guid id,
            string title,
            string description,
            DateTime expiresAt)
        {
            Id = id;
            SetTitle(title);
            SetDescription(description);
            SetCompletePercentage(0);
            SetExpiresDate(expiresAt);
            CreatedAt = DateTime.UtcNow;
        }

        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new CustomException("Title is required.");
            
            Title = title;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void SetCompletePercentage(int percentage)
        {
            if (CompletePercentage == 100)
                throw new CustomException("Task has been done.");
            
            if (CompletePercentage < 0 && CompletePercentage > 100)
                throw new CustomException("Complete percentage is invalid.");

            CompletePercentage = percentage;
        }

        public void SetExpiresDate(DateTime date)
        {
            if (date < DateTime.UtcNow)
                throw new CustomException("Date cannot be from the past.");

            ExpiresAt = date;
        }
    }
}