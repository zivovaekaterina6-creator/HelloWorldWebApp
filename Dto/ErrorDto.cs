namespace HelloWorld.Dto;

public class ErrorDto
{
  public int StatusCode { get; set; }

  public required string Message { get; set; }
  
  public string? ErrorCode { get; set; }
}