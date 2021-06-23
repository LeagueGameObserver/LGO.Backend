using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Core.Model.League.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeagueTurretTierType
    {
        Undefined,
        TopOuter,
        TopInner,
        TopInhibitor,
        MiddleOuter,
        MiddleInner,
        MiddleInhibitor,
        NexusTop,
        NexusBottom,
        BottomOuter,
        BottomInner,
        BottomInhibitor,
    }
}