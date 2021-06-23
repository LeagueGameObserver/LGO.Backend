namespace LGO.Backend.Server.Model.Response.WebSocket
{
    // Server to client only
    public interface IWebSocketPingResponsePayload
    {
        double ConnectionTimeoutInSeconds { get; }
    }
}