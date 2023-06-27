using CRUDTest.Application.Common.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CRUDTest.Presentation.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
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
        object errors = default;
        int statusCode = (int)HttpStatusCode.InternalServerError;

        if (exception is RestException restException)
        {
            statusCode = (int)restException.Code;

            if (restException.Message is string)
                errors = new[] { restException.Message };
        }
        else
            errors =exception.Message;

        _logger.LogError($"{errors} - {exception.Message} - {exception.StackTrace}");

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { errors }));
    }

}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionHandling(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
