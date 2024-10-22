using System.Collections.Generic;

public interface IMessageService
{
    void SendMessage(Message message);
    IEnumerable<Message> GetMessageHistory(string roomId);
}
