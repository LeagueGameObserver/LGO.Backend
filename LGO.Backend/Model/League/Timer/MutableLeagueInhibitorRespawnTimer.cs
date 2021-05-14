using LGO.Backend.Core.Model.League.Structure;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.Timer
{
    internal sealed class MutableLeagueInhibitorRespawnTimer : MutableLeagueTimer, ILeagueInhibitorRespawnTimer
    {
        public override LeagueTimerType Type => LeagueTimerType.InhibitorRespawn;

        public ILeagueInhibitor Inhibitor { get; set; } = NullLeagueInhibitor.Instance;
    }
}