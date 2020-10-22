using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAPI.Contexts.Todos.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> Browse();
        Task<Todo> Get(Guid id);
        Task<List<Todo>> BrowseByDay(DateTime date);
        Task<List<Todo>> BrowseForToday();
        Task<List<Todo>> BrowseForTommorow();
        Task<List<Todo>> BrowseForWeek();
        Task Create(Todo todo);
        Task Update(Todo todo);
        Task Delete(Guid id);
    }
}