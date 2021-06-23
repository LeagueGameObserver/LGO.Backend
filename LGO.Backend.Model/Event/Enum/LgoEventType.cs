using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Model.Event.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LgoEventType
    {
        Undefined,
        LeagueGameStateChanged,
        LeagueGameUpdated,
    }
}