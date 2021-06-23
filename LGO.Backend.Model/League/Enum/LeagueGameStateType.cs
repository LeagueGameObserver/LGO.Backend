using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Model.League.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeagueGameStateType
    {
        Undefined,
        Loading,
        InProgress,
        Ended
    }
}