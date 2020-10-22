using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoAPI.Contexts.Todos.Services
{
    public interface ITodoQueryService
    {
        Task<List<Todo>> Browse();
        Task<List<Todo>> BrowseIncome(string filter);
        Task<Todo> Get(Guid id);
    }
}