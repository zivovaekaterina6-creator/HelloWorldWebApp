namespace HelloWorld.Services.Senders;

public interface IMessageSender
{
    void SendMessage(string message);
}