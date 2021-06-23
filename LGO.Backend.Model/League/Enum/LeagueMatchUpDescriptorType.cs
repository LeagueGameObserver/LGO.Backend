using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Model.League.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeagueMatchUpDescriptorType
    {
        Undefined,
        Player,
        Team,
    }
}