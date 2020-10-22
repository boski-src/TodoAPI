using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TodoAPI.Extensions
{
    public static class ConfigExtension
    {
        public static T GetConfig<T>(this IServiceCollection services, string sectionName)
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetConfig<T>(sectionName);
        }

        public static T GetConfig<T>(this IServiceProvider serviceProvider, string sectionName)
        {
            var configuration = serviceProvider.GetService<IConfiguration>();
            return configuration.GetSection(sectionName).Get<T>();
        }
    }
}