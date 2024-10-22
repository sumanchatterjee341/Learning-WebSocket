using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

public class InMemoryRoomRepository : IRoomRepository
{
    private readonly ConcurrentDictionary<string, Room> _rooms = new();
    //private readonly ConcurrentDictionary<string, ConcurrentBag<WebSocket>> _socketRooms = new();
    private readonly ConcurrentDictionary<WebSocket, string> _socketRooms = new();

    public void AddRoom(Room room) => _rooms.TryAdd(room.Id, room);
    public void DeleteRoom(Guid id) => _rooms.TryRemove(id.ToString(), out var room);


    public Room GetRoom(string roomId) => _rooms.TryGetValue(roomId, out var room) ? room : null;

    public IEnumerable<Room> GetAllRooms() => _rooms.Values.ToList();

    public void LeaveRoom(WebSocket webSocket, string roomName) => _socketRooms.TryRemove(webSocket, out _);

    public void JoinRoom(WebSocket webSocket, string roomName)
    {
        _socketRooms[webSocket] = roomName;
    }

    public string GetRoomIdByWebSocket(WebSocket webSocket) => _socketRooms[webSocket];

    public List<WebSocket> GetAllWebSocketsByRoomId(string roomId) => _socketRooms.Where(
        x => x.Value == roomId)
        .Select(x => x.Key)
        .ToList();

    public bool IsRoomExists(string roomId) => _rooms.ContainsKey(roomId);
}
