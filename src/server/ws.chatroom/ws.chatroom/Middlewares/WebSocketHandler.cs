using System.Net.WebSockets;
using System.Text;
using ws.chatapp.Models;
using ws.chatapp.Services;

namespace ws.chatapp.Middlewares;

public class WebSocketHandler
{
    private readonly IMessageService _messageService;
    private readonly IRoomService _roomService;
   
    private const string GeneralRoom = "general";
    private const string JoinCommand = "/join ";
    private const string LeaveCommand = "/leave ";

    public WebSocketHandler(IMessageService messageService, IRoomService roomService)
    {
        _messageService = messageService;
        _roomService = roomService;
    }

    public async Task HandleWebSocketAsync(WebSocket webSocket, string socketId, string userName)
    {
        _roomService.JoinRoom(webSocket, GeneralRoom);

        var buffer = new byte[1024 * 4];
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var messageContent = Encoding.UTF8.GetString(buffer, 0, result.Count);
            if (result.MessageType == WebSocketMessageType.Text)
            {
                if (messageContent.StartsWith(JoinCommand))
                {
                    var roomName = messageContent.Substring(6);
                    _roomService.JoinRoom(webSocket, roomName);
                    continue;
                }
                else if (messageContent.StartsWith(LeaveCommand))
                {
                    var roomName = messageContent.Substring(7);
                    _roomService.LeaveRoom(webSocket, roomName);
                    continue;
                }

                var message = new Message
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userName,
                    UserId = socketId,
                    RoomId = _roomService.GetRoomIdByWebSocket(webSocket),
                    Content = messageContent,
                    Timestamp = DateTime.UtcNow
                };

                _messageService.SendMessage(message);
                await BroadcastMessage(message.RoomId, message);
            }
        }
    }

    private async Task BroadcastMessage(string roomId, Message message)
    {
        var content = $"{message.UserName}: {message.Content}";
        var encodedMessage = Encoding.UTF8.GetBytes(content);
        var tasks = new List<Task>();

        foreach (var socket in _roomService.GetAllWebSocketsByRoomId(roomId))
        {
            if (socket.State == WebSocketState.Open)
            {
                tasks.Add(socket.SendAsync(new ArraySegment<byte>(encodedMessage), WebSocketMessageType.Text, true, CancellationToken.None));
            }
        }
        await Task.WhenAll(tasks);
    }
}
