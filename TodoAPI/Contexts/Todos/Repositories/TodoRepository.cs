using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoAPI.Extensions.Mongo;

namespace TodoAPI.Contexts.Todos.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IMongoGenericRepository<Todo> _repository;

        public TodoRepository(IMongoGenericRepository<Todo> repository)
        {
            _repository = repository;
        }

        public async Task<List<Todo>> Browse()
        {
            return await _repository.FindMany(x => true);
        }

        public async Task<Todo> Get(Guid id)
        {
            return await _repository.FindOne(id);
        }

        public async Task<List<Todo>> BrowseByDay(DateTime date)
        {
            var day = date.Day;
            var month = date.Month;
            var year = date.Year;
            
            var startDay = new DateTime(year, month, day);
            var endDay = new DateTime(year, month, day, 23, 59 ,59);
            
            // var filter = Builders<Todo>.Filter.And(
            //     Builders<Todo>.Filter.Eq(x => x.ExpiresAt.Day, day),
            //     Builders<Todo>.Filter.Eq(x => x.ExpiresAt.Month, month),
            //     Builders<Todo>.Filter.Eq(x => x.ExpiresAt.Year, year)
            // );

            return await _repository.FindMany(x => x.ExpiresAt >= startDay && x.ExpiresAt <= endDay);
        }

        public async Task<List<Todo>> BrowseForToday()
        {
            return await BrowseByDay(DateTime.UtcNow);
        }

        public async Task<List<Todo>> BrowseForTommorow()
        {
            return await BrowseByDay(DateTime.UtcNow.AddDays(1));
        }

        public async Task<List<Todo>> BrowseForWeek()
        {
            var day = DateTime.UtcNow.DayOfWeek;
            var weekDayStart = day - DayOfWeek.Monday;

            var startDate = DateTime.UtcNow.AddDays(-weekDayStart);
            var endDate = startDate.AddDays(6);

            return await _repository.FindMany(
                x =>
                    x.ExpiresAt >= startDate &&
                    x.ExpiresAt <= endDate
            );
        }

        public async Task Create(Todo todo)
        {
            await _repository.Create(todo);
        }

        public async Task Update(Todo todo)
        {
            await _repository.Update(todo);
        }

        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }
    }
}