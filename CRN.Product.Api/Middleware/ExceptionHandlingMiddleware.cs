using System.Net;
using System.Text.Json;
using CRN.Product.Application.DTOs.Common;
using Serilog;

namespace CRN.Product.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.Unauthorized,
                ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            await HandleExceptionAsync(
                context,
                HttpStatusCode.NotFound,
                ex.Message);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Unhandled exception occurred.");

            await HandleExceptionAsync(
                context,
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred.");
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new ErrorResponseDto
        {
            StatusCode = (int)statusCode,
            Message = message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}