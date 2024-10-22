using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace ws.chatapp.Models
{
    public class WebSocketRoom
    {
        public string RoomId { get; set; }
        public ConcurrentBag<WebSocket> Connections { get; set; } = new ConcurrentBag<WebSocket>();
    }
}
