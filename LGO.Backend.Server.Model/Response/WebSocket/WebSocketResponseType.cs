using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Server.Model.Response.WebSocket
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum WebSocketResponseType
    {
        Undefined,
        
        Ping,
    }
}