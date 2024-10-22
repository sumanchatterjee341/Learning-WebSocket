using System.Collections.Generic;
using System.Net.WebSockets;

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
