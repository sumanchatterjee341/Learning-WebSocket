using System.Net.WebSockets;
using ws.chatapp.Models;

namespace ws.chatapp.Services;

public interface IRoomService
{
    void CreateRoom(string name);
    void DeleteRoom(Guid id);
    Room GetRoom(string roomId);
    IEnumerable<Room> GetAllRooms();
    void LeaveRoom(WebSocket webSocket, string roomName);
    void JoinRoom(WebSocket webSocket, string roomName);
    string GetRoomIdByWebSocket(WebSocket webSocket);
    List<WebSocket> GetAllWebSocketsByRoomId(string roomId);
}
