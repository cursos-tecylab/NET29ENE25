using Microsoft.Extensions.DependencyInjection;
using Usuario.Domain.Usuarios;

namespace Usuario.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR( configuration => {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddTransient<NombreUsuarioServices>(); // Crea una instancia por vez que se solicita
        return services;
    }
}