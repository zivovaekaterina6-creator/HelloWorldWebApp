namespace HelloWorld.Services.Senders;

public class SmsSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Send Message {message} by SMS");
    }
}