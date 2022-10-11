using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Lapka.Messages.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ProjectException ex)
        {
            context.Response.StatusCode = ex._errorCode;
            context.Response.Headers.Add("content-type", "application/json");

            var errorCode = ex._errorCode;
            var json = JsonSerializer.Serialize(new { ErrorCode = errorCode, ex.Message });
            await context.Response.WriteAsync(json);
        }
    }
}