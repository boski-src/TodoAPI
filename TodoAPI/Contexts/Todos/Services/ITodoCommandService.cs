using System;
using System.Threading.Tasks;

namespace TodoAPI.Contexts.Todos.Services
{
    public interface ITodoCommandService
    {
        Task Create(Guid id, string title, string description, DateTime expiresAt);
        Task Update(Guid id, string title, string description, DateTime expiresAt);
        Task UpdateCompletePercentage(Guid id, int percentage);
        Task MarkAsDone(Guid id);
        Task Delete(Guid id);
    }
}