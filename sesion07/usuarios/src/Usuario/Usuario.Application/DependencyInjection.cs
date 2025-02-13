using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Usuario.Application.Abstractions.Behaviors;
using Usuario.Domain.Usuarios;

namespace Usuario.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR( configuration => {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });

        services.AddTransient<NombreUsuarioServices>(); // Crea una instancia por vez que se solicita
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);



        return services;
    }
}