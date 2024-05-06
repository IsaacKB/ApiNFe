using pocApiSefaz.Repositories.Interfaces;
using pocApiSefaz.Repositories;
using pocApiSefaz.Services.Interfaces;
using pocApiSefaz.Services;
using AutoMapper;

namespace pocApiSefaz.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<Mapper>();

            return services;
        }
    }
}
