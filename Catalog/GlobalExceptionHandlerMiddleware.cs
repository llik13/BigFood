using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Catalog.WebApi
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
                await HandleExceptionAsync(context, ex);
            }

            if(!context.Response.HasStarted && context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                await HandleUnauthorizedAsync(context);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = ex.Message
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)problemDetails.Status;

            return context.Response.WriteAsJsonAsync(problemDetails);
        }

        private static Task HandleUnauthorizedAsync(HttpContext context)
        {
            context.Response.ContentType = "application/problem+json";

            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                error = "Unauthorized. Invaild token or insufficient permission."
            }));

        }
    }

}
