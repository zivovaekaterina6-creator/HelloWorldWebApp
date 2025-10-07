namespace HelloWorld.Services.Senders;

public class TelegramSender : IMessageSender
{
    public void SendMessage(string message)
    {
        Console.WriteLine($"Send Message {message} by Telegram");
    }
}