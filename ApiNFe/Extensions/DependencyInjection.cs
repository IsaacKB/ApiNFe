using ApiNFe.Repositories.Interfaces;
using ApiNFe.Repositories;
using ApiNFe.Services.Interfaces;
using ApiNFe.Services;
using AutoMapper;

namespace ApiNFe.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ISoapService, SoapService>();
            services.AddScoped<IRabbitMQService, RabbitMQService>();

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
