using System.Collections.Generic;

public interface IMessageRepository
{
    void AddMessage(Message message);
    IEnumerable<Message> GetMessages(string roomId);
}
