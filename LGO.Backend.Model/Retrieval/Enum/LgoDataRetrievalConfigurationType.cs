using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Model.Retrieval.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LgoDataRetrievalConfigurationType
    {
        Undefined,
        LeagueItem,
        LeagueChampion,
        LeaguePlayer,
        LeagueTeam,
        LeagueTimer,
        LeaguePowerPlayTimer,
        LeagueGame,
    }
}