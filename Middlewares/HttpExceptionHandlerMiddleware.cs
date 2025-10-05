using System.Text.Json;
using HelloWorld.Dto;
using HelloWorld.Exceptions;

namespace HelloWorld.Middlewares;

public class HttpExceptionHandlerMiddleware
{
  private readonly RequestDelegate _next;

  public HttpExceptionHandlerMiddleware(RequestDelegate next, ILogger<HttpExceptionHandlerMiddleware> logger)
  {
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
      await ProcessException(context, ex);
    }
  }

  public async Task ProcessException(HttpContext context, Exception ex)
  {
    switch (ex)
    {
      case HttpException httpException:
        await ProcessError(context, httpException.StatusCode, httpException.Message);
        break;
      default:
        await ProcessError(context, StatusCodes.Status500InternalServerError, $"Непредвиденная ошибка сервера: {ex.Message}");
        break;
    }
  }

  private async Task ProcessError(HttpContext context, int statusCode, string message)
  {
    var errorDto = new ErrorDto
    {
      StatusCode = statusCode,
      Message = message
    };
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = statusCode;
    var jsonResponse = JsonSerializer.Serialize(errorDto);
    await context.Response.WriteAsync(jsonResponse);
  }
}