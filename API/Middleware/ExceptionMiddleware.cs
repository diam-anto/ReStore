using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        // logger to log any exception, HostEnvironment to check if we are in development mode or production 
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, 
        IHostEnvironment env) 
        {
            _env = env;
            _logger = logger;
            _next = next;

        }

        // our framework excepts our middleware to have this particular method
        public async Task InvokeAsync(HttpContext context) 
        {
            // to catch any exception 
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500; // internal server error

                var response = new ProblemDetails
                {
                    Status = 500,
                    Detail = _env.IsDevelopment() ? ex.StackTrace?.ToString() : null, //check if we are in dev mode
                    Title = ex.Message
                };

                // outside of an API controller we should specify this options
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                // create a json response
                var json = JsonSerializer.Serialize(response, options);

                // what we return to the client if we have exception
                await context.Response.WriteAsync(json);

            }

        }
    }
}