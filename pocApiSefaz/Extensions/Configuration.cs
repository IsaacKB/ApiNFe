using Carter;
using Microsoft.EntityFrameworkCore;
using pocApiSefaz.Context;

namespace pocApiSefaz.Extensions
{
    public static class Configuration
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("DbTest"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddRepositories();
            builder.Services.AddServices();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCarter();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void RegisterMiddlewares(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapCarter();
        }
    }
}
