using ws.chatapp.Models;

namespace ws.chatapp.Repositories;

public interface IMessageRepository
{
    void AddMessage(Message message);
    IEnumerable<Message> GetMessages(string roomId);
}
