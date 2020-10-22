using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoAPI.Types;

namespace TodoAPI.Extensions.Mongo
{
    public interface IMongoGenericRepository<TEntity> where TEntity : IEntity
    {
        IMongoCollection<TEntity> Collection { get; set; }

        Task Create(TEntity entity);
        Task<TEntity> FindOne(Guid id);
        Task<TEntity> FindOne(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> FindOne(FilterDefinition<TEntity> filter);
        Task<List<TEntity>> FindMany(Expression<Func<TEntity, bool>> criteria);
        Task<List<TEntity>> FindMany(FilterDefinition<TEntity> filter);
        Task Update(TEntity entity);
        Task Delete(Guid id);
        Task Delete(Expression<Func<TEntity, bool>> criteria);
    }
}