namespace HelloWorld.Exceptions;

public class HttpException : Exception
{
    public int StatusCode { get; init; }
    
    public string Message { get; init; }
    
    public string? ErrorCode { get; init; }

    public HttpException(int statusCode, string message, string? errorCode)
    {
        StatusCode = statusCode;
        Message = message;
        ErrorCode = errorCode;
    }
}