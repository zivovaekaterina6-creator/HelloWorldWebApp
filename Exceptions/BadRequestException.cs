namespace HelloWorld.Exceptions;

public class BadRequestException : HttpException
{
    public BadRequestException(string message, string? errorCode = null) 
        : base(StatusCodes.Status400BadRequest, message, errorCode)
    {
    }
}