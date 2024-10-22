using System.Collections.Concurrent;
using ws.chatapp.Models;

namespace ws.chatapp.Repositories;

public class InMemoryMessageRepository : IMessageRepository
{
    private readonly ConcurrentBag<Message> _messages = new();

    public void AddMessage(Message message) => _messages.Add(message);

    public IEnumerable<Message> GetMessages(string roomId) =>
        _messages.Where(m => m.RoomId == roomId);
}
