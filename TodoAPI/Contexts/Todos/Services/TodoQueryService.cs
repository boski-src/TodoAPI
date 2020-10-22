using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoAPI.Contexts.Todos.Repositories;

namespace TodoAPI.Contexts.Todos.Services
{
    public class TodoQueryService : ITodoQueryService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoQueryService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<List<Todo>> Browse()
        {
            return await _todoRepository.Browse();
        }

        public async Task<List<Todo>> BrowseIncome(string filter)
        {
            return filter switch {
                TodoFilter.Today => await _todoRepository.BrowseForToday(),
                TodoFilter.Tommorow => await _todoRepository.BrowseForTommorow(),
                TodoFilter.Week => await _todoRepository.BrowseForWeek(),
                _ => await _todoRepository.Browse()
            };
        }

        public async Task<Todo> Get(Guid id)
        {
            return await _todoRepository.Get(id);
        }
    }
}