using LGO.Backend.Server.Model.Request.WebSocket.Converter;
using Newtonsoft.Json;

namespace LGO.Backend.Server.Model.Request.WebSocket
{
    [JsonConverter(typeof(WebSocketRequestConverter))]
    internal sealed class MutableWebSocketRequest : IWebSocketRequest
    {
        public WebSocketRequestType Type { get; set; } = WebSocketRequestType.Undefined;
        public object? Payload { get; set; }
    }
}