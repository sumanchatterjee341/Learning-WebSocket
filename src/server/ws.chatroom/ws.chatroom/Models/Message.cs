namespace ws.chatapp.Models;

public class Message
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string RoomId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }

}
