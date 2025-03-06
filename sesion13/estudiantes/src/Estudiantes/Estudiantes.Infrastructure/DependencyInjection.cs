using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Estudiantes.Application.Abstractions.Clock;
using Estudiantes.Domain.Abstractions;
using Estudiantes.Domain.Estudiantes;
using Estudiantes.Infrastructure.Clock;
using Estudiantes.Infrastructure.Repositories;
using Estudiantes.Domain.Matriculas;
using StackExchange.Redis;
using Estudiantes.Application.Services;
using Estudiantes.Infrastructure.Services;
using Estudiantes.Domain.Programaciones;
namespace Estudiantes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        var connectionStringPostgres = configuration.GetConnectionString("Database")
    ?? throw new ArgumentNullException(nameof(configuration));

        var connectionStringRedis = configuration.GetConnectionString("Redis")
        ?? throw new ArgumentNullException(nameof(configuration));

        var usuariosApiBaseUrl = configuration["UsuariosApiBaseUrl"];
        var cursosApiBaseUrl = configuration["CursosApiBaseUrl"];
        var docentesApiBaseUrl = configuration["DocentesApiBaseUrl"];

        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseNpgsql(connectionStringPostgres).UseSnakeCaseNamingConvention(); // usuario, producto_detalle
            }
        );

        services.AddSingleton<IConnectionMultiplexer>(sp =>
       {
           var configurationRedis = ConfigurationOptions.Parse(connectionStringRedis);
           return ConnectionMultiplexer.Connect(configurationRedis);
       });

        services.AddScoped<IEstudianteRepository, EstudianteRepository>();
        services.AddScoped<IMatriculaRepository, MatriculaRepository>();
        services.AddScoped<IProgramacionRepository, ProgramacionRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddHttpClient<IUsuariosService, UsuarioService>(client =>
       {
           client.BaseAddress = new Uri(usuariosApiBaseUrl!);
       });

        services.AddHttpClient<ICursosService, CursoService>(client =>
        {
            client.BaseAddress = new Uri(cursosApiBaseUrl!);
        });

        services.AddHttpClient<IDocentesService, DocenteService>(client =>
        {
            client.BaseAddress = new Uri(docentesApiBaseUrl!);
        });

        services.AddScoped<ICacheService, RedisCacheService>();
        services.AddSingleton<IEventBus, RabbitMQEventBus>();
        services.AddHostedService<RabbitMQEventListener>();

        return services;
    }

}
