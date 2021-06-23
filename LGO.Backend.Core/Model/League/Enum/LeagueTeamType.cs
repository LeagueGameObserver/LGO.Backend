using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Core.Model.League.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeagueTeamType
    {
        Undefined,
        Blue,
        Red,
        Neutral,
    }
}