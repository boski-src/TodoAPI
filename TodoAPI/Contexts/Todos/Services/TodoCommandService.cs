using System;
using System.Threading.Tasks;
using TodoAPI.Contexts.Todos.Repositories;
using TodoAPI.Types;

namespace TodoAPI.Contexts.Todos.Services
{
    public class TodoCommandService : ITodoCommandService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoCommandService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task Create(Guid id, string title, string description, DateTime expiresAt)
        {
            var todo = new Todo(id, title, description, expiresAt);

            await _todoRepository.Create(todo);
        }

        public async Task Update(Guid id, string title, string description, DateTime expiresAt)
        {
            var todo = await GetAndValidate(id);

            todo.SetTitle(title);
            todo.SetDescription(description);
            todo.SetExpiresDate(expiresAt);

            await _todoRepository.Update(todo);
        }

        public async Task UpdateCompletePercentage(Guid id, int percentage)
        {
            var todo = await GetAndValidate(id);

            todo.SetCompletePercentage(percentage);

            await _todoRepository.Update(todo);
        }

        public async Task MarkAsDone(Guid id)
        {
            await UpdateCompletePercentage(id, 100);
        }

        public async Task Delete(Guid id)
        {
            await _todoRepository.Delete(id);
        }

        private async Task<Todo> GetAndValidate(Guid id)
        {
            var task = await _todoRepository.Get(id);
            if (task is null)
                throw new ServiceException("Todo task not found.");

            return task;
        }
    }
}