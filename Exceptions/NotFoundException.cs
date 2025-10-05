namespace HelloWorld.Exceptions;

public class NotFoundException: HttpException
{
    public NotFoundException(string message, string? errorCode = null) 
        : base(StatusCodes.Status404NotFound, message, errorCode)
    {
    }
}