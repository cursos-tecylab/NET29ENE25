using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

public class RoleMiddleware
{
    private readonly RequestDelegate _next;

    public RoleMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // public async Task InvokeAsync(HttpContext context)
    // {
    //     var path = context.Request.Path;

    //     if (path.StartsWithSegments("/api/auth"))
    //     {
    //          await _next(context);
    //          return;
    //     }

    //     var user = context.User;
    //     if (user.Identity != null && user.Identity.IsAuthenticated && user.IsInRole("Admin"))
    //     {
    //         await _next(context);
    //     }
    //     else
    //     {
    //         context.Response.StatusCode = 403;
    //         await context.Response.WriteAsync("Acceso denegado: No tienes permisos suficientes");
    //     }
    // }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint == null)
        {
            await _next(context);
            return;
        }

        var allowAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>() != null;
        var authorize = endpoint.Metadata.GetMetadata<AuthorizeAttribute>();

        if (allowAnonymous)
        {
            await _next(context);
            return;
        }

        var user = context.User;

        if (authorize != null)
        {
            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("No estas autenticado");
                return;
            }

            var requiredRoles = authorize.Roles?.Split(',') ?? new string[0];

            if (requiredRoles.Length > 0 && !requiredRoles.Any(role => user.IsInRole(role.Trim())))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Acceso denegado: No tienes permisos suficientes");
                return;
            }
        }

        await _next(context);
    }
}