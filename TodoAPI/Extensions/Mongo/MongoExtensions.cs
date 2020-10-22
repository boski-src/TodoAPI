using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using TodoAPI.Types;

namespace TodoAPI.Extensions.Mongo
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            var config = services.GetConfig<MongoDbConfig>("Mongo");
            services.AddSingleton(config);
            services.AddSingleton(new MongoClient(config.ConnectionString));
            services.AddScoped(
                ctx => {
                    var client = ctx.GetService<MongoClient>();
                    return client.GetDatabase(config.Database);
                }
            );

            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            return services;
        }

        public static IServiceCollection AddMongoRepository<TEntity>(this IServiceCollection services,
            string collectionName)
        where TEntity : IEntity
        {
            services.AddScoped<IMongoGenericRepository<TEntity>>(
                ctx => new MongoGenericRepository<TEntity>(
                    ctx.GetService<IMongoDatabase>().GetCollection<TEntity>(collectionName)
                )
            );

            return services;
        }
    }
}