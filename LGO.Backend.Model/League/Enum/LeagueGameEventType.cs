using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LGO.Backend.Model.League.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum LeagueGameEventType
    {
        Undefined,
        GameStarted,
        TurretDestroyed,
        InhibitorDestroyed,
        DragonKilled,
        RiftHeraldKilled,
        BaronNashorKilled,
        ChampionKilled,
        MultipleChampionsKilled,
        TeamKilled,
        ItemsChanged,
        GameEnded
    }
}