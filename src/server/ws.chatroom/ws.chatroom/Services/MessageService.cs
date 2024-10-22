using ws.chatapp.Models;
using ws.chatapp.Repositories;

namespace ws.chatapp.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public void SendMessage(Message message)
    {
        _messageRepository.AddMessage(message);
    }

    public IEnumerable<Message> GetMessageHistory(string roomId)
    {
        return _messageRepository.GetMessages(roomId);
    }
}
