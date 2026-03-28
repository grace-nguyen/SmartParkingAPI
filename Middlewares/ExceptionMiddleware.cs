using System.Net;
using System.Text.Json;
using SmartParkingApi.Models;

namespace SmartParkingApi.Middlewares;

public class ExceptionMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;
  private readonly IHostEnvironment _env;

  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
  {
    _next = next;
    _logger = logger;
    _env = env;
  }

  public async Task InvokeAsync(HttpContext context)

  {
    try
    {
      await _next(context);

    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An unhandled exception occurred.");

      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      var errorResponse = new ErrorResponse
      {
        StatusCode = context.Response.StatusCode,
        Message = "Internal Server Error. Please try again later.",
        Details = _env.IsDevelopment() ? ex.ToString() : null
      };

      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
      var json = JsonSerializer.Serialize(errorResponse, options);

      await context.Response.WriteAsync(json);
    }
  }
}