using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Model.League.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeagueTimerType
    {
        Undefined,
        DragonRespawn,
        RiftHeraldRespawn,
        BaronNashorRespawn,
        InhibitorRespawn,
        BaronNashorPowerPlay,
        ElderDragonPowerPlay,
    }
}