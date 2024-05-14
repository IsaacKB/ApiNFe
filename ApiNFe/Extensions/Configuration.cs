using Carter;
using Microsoft.EntityFrameworkCore;
using ApiNFe.Context;

namespace ApiNFe.Extensions
{
    public static class Configuration
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(options => 
                options.UseNpgsql(connectionString)
            );
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
