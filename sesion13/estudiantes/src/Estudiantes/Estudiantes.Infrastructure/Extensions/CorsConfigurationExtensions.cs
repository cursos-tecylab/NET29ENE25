using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Estudiantes.Infrastructure.Extensions
{
    public static class CorsConfigurationExtensions
    {
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
            var corsPolicy = BuildCorsPolicy(allowedOrigins!);

            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", corsPolicy);
            });

            return services;
        }

        private static Action<CorsPolicyBuilder> BuildCorsPolicy(string[] allowedOrigins)
        {
            return builder =>
            {
                builder
                    .WithOrigins(allowedOrigins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            };
        }
    }
}
