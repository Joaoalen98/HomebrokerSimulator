using HomebrokerSimulator.ClientAPI.Common.Exceptions;

namespace HomebrokerSimulator.ClientAPI.Common.Middlewares;

public class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro inesperado.");
            await HandleException(ex, httpContext);
        }
    }

    private static async Task HandleException(Exception ex, HttpContext context)
    {
        if (ex is NotFoundException)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync(new ErrorMessageDTO(ex.Message));
        }
        else if (ex is BadRequestException)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new ErrorMessageDTO(ex.Message));
        }
        else
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new ErrorMessageDTO("Ocorreu um erro inesperado."));
        }
    }
}

public record ErrorMessageDTO(string Message);
