using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Core.Model.League.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeagueGameModeType
    {
        Undefined,
        Classic,
        Aram,
        PracticeTool,
    }
}