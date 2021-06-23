namespace LGO.Backend.Server.Model.Response.WebSocket
{
    public interface IWebSocketResponse
    {
        WebSocketResponseType Typ { get; }

        object? Payload { get; }
    }
}