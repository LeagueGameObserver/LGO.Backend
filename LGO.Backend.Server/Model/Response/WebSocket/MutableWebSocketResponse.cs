namespace LGO.Backend.Server.Model.Response.WebSocket
{
    internal sealed class MutableWebSocketResponse : IWebSocketResponse
    {
        public WebSocketResponseType Typ { get; set; } = WebSocketResponseType.Undefined;
        public object? Payload { get; set; }
    }
}