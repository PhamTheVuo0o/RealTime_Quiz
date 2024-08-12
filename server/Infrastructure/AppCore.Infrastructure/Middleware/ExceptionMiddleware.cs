using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using AppCore.Infrastructure.Models;
using AppCore.Infrastructure.Extensions;

namespace AppCore.Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, 
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _env = env;
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
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                string message = string.Empty;

                if (ex is UnauthorizedAccessException)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    message = "";
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    message = "Internal Server Error";
                }

                var response = _env.IsDevEnv()
                    ? new ExceptionResponse(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ExceptionResponse(context.Response.StatusCode, message);

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}