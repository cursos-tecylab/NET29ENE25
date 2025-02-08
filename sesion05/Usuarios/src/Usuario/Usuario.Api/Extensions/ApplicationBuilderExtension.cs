using Microsoft.EntityFrameworkCore;
using Usuario.Infrastructure;

namespace Usuario.Api.Extensions;

public static class ApplicationBuilderExtension
{
    public static async void ApplyMigrations(
        this IApplicationBuilder app
    )
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var service = scope.ServiceProvider;
            var loggerFactory = service.GetRequiredService<ILoggerFactory>();

            try
            {
                var context = service.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,"Ocurrio un error en la migracion de la capa dominio a la base de datos");
                throw;
            }

        }
    }
}