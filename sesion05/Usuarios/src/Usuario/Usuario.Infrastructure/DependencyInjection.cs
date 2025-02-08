using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Usuario.Application.Abstractions.Data;
using Usuario.Application.Abstractions.Email;
using Usuario.Application.Abstractions.Time;
using Usuario.Domain.Abstractions;
using Usuario.Domain.Roles;
using Usuario.Domain.Usuarios;
using Usuario.Infrastructure.Abstractions.Data;
using Usuario.Infrastructure.Abstractions.Email;
using Usuario.Infrastructure.Abstractions.Time;
using Usuario.Infrastructure.Repositories;

namespace Usuario.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        
        services.AddTransient<IDateTimeProvider,DateTimeProvider>();  
        services.AddTransient<IEmailService,EmailService>();   

        var connectionString = configuration.GetConnectionString("Database") 
        ?? throw new ArgumentNullException("Database connection string not found"); 

        services.AddDbContext<ApplicationDbContext>(options =>
           {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
           }
        );

        services.AddScoped<IUsuarioRepository,UsuarioRepository>();
        services.AddScoped<IRolRepository,RolRepository>(); 

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>
        (
            _ => new SqlConnectionFactory(connectionString)
        );

        return services;
    }
}