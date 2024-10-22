public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;
    private readonly WebSocketHandler _webSocketHandler;

    public WebSocketMiddleware(RequestDelegate next, WebSocketHandler webSocketHandler)
    {
        _next = next;
        _webSocketHandler = webSocketHandler;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var socketId = context.Connection.Id;
            var currentUser = context.Request.Query["name"];
            await _webSocketHandler.HandleWebSocketAsync(webSocket, socketId, currentUser);
        }
        else
        {
            await _next(context);
        }
    }
}
