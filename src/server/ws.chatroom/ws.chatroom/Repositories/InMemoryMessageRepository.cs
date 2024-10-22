using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

public class InMemoryMessageRepository : IMessageRepository
{
    private readonly ConcurrentBag<Message> _messages = new();

    public void AddMessage(Message message) => _messages.Add(message);

    public IEnumerable<Message> GetMessages(string roomId) =>
        _messages.Where(m => m.RoomId == roomId);
}
