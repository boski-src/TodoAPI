using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace TodoAPI.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddOpenApiDocument(
                c => {
                    c.PostProcess = document => {
                        document.Info.Version = "v1";
                        document.Info.Title = "Todo API";
                        document.GenerateOperationIds();
                    };

                    c.OperationProcessors.Add(new FlattenOperationsProcessor());
                }
            );

            return services;
        }

        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi3();

            return app;
        }

        class FlattenOperationsProcessor : IOperationProcessor
        {
            public bool Process(OperationProcessorContext context)
            {
                context.OperationDescription.Operation.OperationId = context.MethodInfo.Name;
                return true;
            }
        }
    }
}