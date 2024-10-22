using System.Net.WebSockets;
using ws.chatapp.Models;
using ws.chatapp.Repositories;

namespace ws.chatapp.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void CreateRoom(string name)
    {
        var room = new Room { Id = Guid.NewGuid().ToString(), Name = name };
        _roomRepository.AddRoom(room);
    }
    public void DeleteRoom(Guid id) => _roomRepository.DeleteRoom(id);

    public Room GetRoom(string roomId) => _roomRepository.GetRoom(roomId);

    public IEnumerable<Room> GetAllRooms() => _roomRepository.GetAllRooms();
    public void LeaveRoom(WebSocket webSocket, string roomName) => _roomRepository.LeaveRoom(webSocket, roomName);

    public void JoinRoom(WebSocket webSocket, string roomName)
    {
        CreateRoom(roomName);
        _roomRepository.JoinRoom(webSocket, roomName);
    }

    public string GetRoomIdByWebSocket(WebSocket webSocket) => _roomRepository.GetRoomIdByWebSocket(webSocket);
    public List<WebSocket> GetAllWebSocketsByRoomId(string roomId) => _roomRepository.GetAllWebSocketsByRoomId(roomId);
}
