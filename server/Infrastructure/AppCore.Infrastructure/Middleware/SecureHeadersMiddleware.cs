using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using AppCore.Infrastructure.Models;

namespace AppCore.Infrastructure.Middleware
{
    public class SecureHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        public SecureHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
            await _next(context);
        }
    }
}