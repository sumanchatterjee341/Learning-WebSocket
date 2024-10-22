using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IRoomService _roomService;

    public ChatController(IMessageService messageService, IRoomService roomService)
    {
        _messageService = messageService;
        _roomService = roomService;
    }

    [HttpGet("{roomId}/history")]
    public IActionResult GetHistory(string roomId)
    {
        var messages = _messageService.GetMessageHistory(roomId);
        return Ok(messages);
    }

    [HttpPost("rooms")]
    public IActionResult CreateRoom([FromQuery] string name)
    {
        _roomService.CreateRoom(name);
        return Ok();
    }
    [HttpDelete("rooms")]
    public IActionResult DeleteRoom([FromQuery] Guid roomId)
    {
        _roomService.DeleteRoom(roomId);
        return Ok();
    }
    [HttpGet("rooms")]
    public IActionResult GetRooms()
    {
        var rooms = _roomService.GetAllRooms();
        return Ok(rooms);
    }
}
