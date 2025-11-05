using Celebrai.Communication.Responses;
using Celebrai.Exceptions.ExceptionsBase;
using System.Text.Json;

namespace Celebrai.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception is CelebraiException celebraiException)
        {
            context.Response.StatusCode = (int)celebraiException.GetStatusCode();

            var responseJson = new ResponseErrorJson(celebraiException.GetErrorMessages());

            var json = JsonSerializer.Serialize(responseJson);

            _logger.LogWarning(exception, "Exceção de negócio capturada.");

            await context.Response.WriteAsync(json);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var responseJson = new ResponseErrorJson(new List<string>()
            {
                "Erro desconhecido"
            });

            var json = JsonSerializer.Serialize(responseJson);

            _logger.LogCritical(exception, "Erro inesperado capturado");
            await context.Response.WriteAsync(json);
        }

    }
}
