using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using TodoAPI.Types;

namespace TodoAPI.Extensions.Mongo
{
    public class MongoGenericRepository<TEntity> : IMongoGenericRepository<TEntity> where TEntity : IEntity
    {
        public IMongoCollection<TEntity> Collection { get; set; }

        public MongoGenericRepository(IMongoCollection<TEntity> collection)
        {
            Collection = collection;
        }

        public async Task Create(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task<TEntity> FindOne(Guid id)
        {
            return await FindOne(x => x.Id == id);
        }

        public async Task<TEntity> FindOne(Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection.Find(criteria).SingleOrDefaultAsync();
        }

        public async Task<TEntity> FindOne(FilterDefinition<TEntity> filter)
        {
            return await Collection.Find(filter).SingleOrDefaultAsync();
        }


        public async Task<List<TEntity>> FindMany(Expression<Func<TEntity, bool>> criteria)
        {
            return await Collection.Find(criteria).ToListAsync();
        }

        public async Task<List<TEntity>> FindMany(FilterDefinition<TEntity> filter)
        {
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task Update(TEntity entity)
        {
            await Collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        }

        public async Task Delete(Guid id)
        {
            await Collection.DeleteOneAsync(x => x.Id == id);
        }
        
        public async Task Delete(Expression<Func<TEntity, bool>> criteria)
        {
            await Collection.DeleteOneAsync(criteria);
        }
    }
}