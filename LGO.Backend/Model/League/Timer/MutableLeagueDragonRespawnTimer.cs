using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    internal sealed class MutableLeagueDragonRespawnTimer : MutableLeagueTimer, ILeagueDragonRespawnTimer
    {
        public override LeagueTimerType Type => LeagueTimerType.DragonRespawn;
    }
}