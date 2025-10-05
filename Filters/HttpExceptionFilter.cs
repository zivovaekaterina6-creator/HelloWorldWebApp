using System.Net.Mime;
using System.Text.Json;
using HelloWorld.Dto;
using HelloWorld.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HelloWorld.Filters;

public class HttpExceptionFilter : IExceptionFilter
{
  public void OnException(ExceptionContext context)
  {
    switch (context.Exception)
    {
      case HttpException httpException:
        ProcessError(context, httpException.StatusCode, httpException.Message);
        break;
      case { } ex:
        ProcessError(context, 
          StatusCodes.Status500InternalServerError,
          $"Непредвиденная ошибка сервера: {ex.Message}");
        break;
    }
  }

  private void ProcessError(
    ExceptionContext context, int statusCode, string message)
  {
        var errorDto = new ErrorDto
        {
          StatusCode = statusCode,
          Message = message
        };
        
        context.Result = new ContentResult
        {
          StatusCode = statusCode,
          Content = JsonSerializer.Serialize(errorDto),
          ContentType = MediaTypeNames.Application.Json
        };
        context.ExceptionHandled = true;
 }
}