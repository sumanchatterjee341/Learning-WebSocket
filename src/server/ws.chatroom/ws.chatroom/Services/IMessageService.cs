using ws.chatapp.Models;

namespace ws.chatapp.Services;

public interface IMessageService
{
    void SendMessage(Message message);
    IEnumerable<Message> GetMessageHistory(string roomId);
}
