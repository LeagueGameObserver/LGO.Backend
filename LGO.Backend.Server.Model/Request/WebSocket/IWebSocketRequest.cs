namespace LGO.Backend.Server.Model.Request.WebSocket
{
    public interface IWebSocketRequest
    {
        WebSocketRequestType Type { get; }
        
        object? Payload { get; }
    }
}