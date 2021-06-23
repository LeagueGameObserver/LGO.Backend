namespace LGO.Backend.Server.Model.Response.WebSocket
{
    internal sealed class MutableWebSocketPingResponsePayload : IWebSocketPingResponsePayload
    {
        public double ConnectionTimeoutInSeconds { get; set; } = Constants.WebSocketConnectionTimeout.TotalSeconds;
    }
}