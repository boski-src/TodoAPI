using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TodoAPI.Contexts.Todos;
using TodoAPI.Contexts.Todos.Repositories;
using TodoAPI.Contexts.Todos.Services;
using TodoAPI.Extensions;
using TodoAPI.Extensions.Mongo;

namespace TodoAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddCustomSwagger();
            services.AddMongo()
                    .AddMongoRepository<Todo>("todos");

            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<ITodoQueryService, TodoQueryService>();
            services.AddTransient<ITodoCommandService, TodoCommandService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCustomSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}