using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Server.Model.Request.WebSocket
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WebSocketRequestType
    {
        Undefined,
        
        Ping,
    }
}