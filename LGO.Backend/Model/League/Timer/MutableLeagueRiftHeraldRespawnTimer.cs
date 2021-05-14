using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    internal sealed class MutableLeagueRiftHeraldRespawnTimer : MutableLeagueTimer, ILeagueRiftHeraldRespawnTimer
    {
        public override LeagueTimerType Type => LeagueTimerType.RiftHeraldRespawn;
    }
}