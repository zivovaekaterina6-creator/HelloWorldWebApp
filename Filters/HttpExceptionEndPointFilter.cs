using System.Net.Mime;
using System.Text.Json;
using HelloWorld.Dto;
using HelloWorld.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HelloWorld.Filters;

public class HttpExceptionEndPointFilter : IEndpointFilter
{
  public async ValueTask<object?> InvokeAsync(
    EndpointFilterInvocationContext context,
    EndpointFilterDelegate next)
  {
    try
    {
      return await next(context);
    }
    catch (Exception ex)
    {
      return ProcessException(ex);
    }
  }

  private IResult ProcessException(Exception ex)
  {
    return ex switch
    {
      HttpException httpException => 
        GetErrorResult(
          httpException.StatusCode, 
          httpException.Message),
      
      _ => GetErrorResult(
        StatusCodes.Status500InternalServerError,
        $"Непредвиденная ошибка сервера: {ex.Message}")
    };
  }

  private IResult GetErrorResult(
    int statusCode, string message)
  {
    var error = new ErrorDto
    {
      StatusCode = statusCode,
      Message = message
    };

    return Results.Json(error, statusCode: statusCode);
  }
}