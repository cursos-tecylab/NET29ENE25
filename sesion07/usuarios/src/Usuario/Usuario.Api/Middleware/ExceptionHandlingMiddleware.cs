using Microsoft.AspNetCore.Mvc;
using Usuario.Application.Exceptions;

namespace Usuario.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Ocurrio una excepcion: {Message}" , ex.Message);
            var exceptionDetail = GetExceptionDetail(ex);
            var problemDetail = new ProblemDetails{
                Status = exceptionDetail.Status,
                Type = exceptionDetail.Type,
                Title = exceptionDetail.Title,
                Detail = exceptionDetail.Detail
            };

           if (exceptionDetail.Errors is not null)
           {
             problemDetail.Extensions["errors"] = exceptionDetail.Errors;
           }
           context.Response.StatusCode = exceptionDetail.Status;
           await context.Response.WriteAsJsonAsync(problemDetail);
        }
    }

    private static ExceptionDetail GetExceptionDetail(Exception exception)
    {
        return exception switch
        {
            ValidationExceptions validationException => new ExceptionDetail(
                StatusCodes.Status400BadRequest,
                "ValidacionFallo",
                "Validacion de Error",
                "Ha ocurrido errores al validar",
                 validationException.Errors
            ),
            _ => new ExceptionDetail(
                StatusCodes.Status500InternalServerError,
                "ServerError",
                "Error en el servidor",
                "Ocurrio un errore inesperado",
                null
            )
        };
    }

    internal record ExceptionDetail
    (
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors
    );
    



}
