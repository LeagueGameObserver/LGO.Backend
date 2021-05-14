using LGO.Backend.Core.Model.League.Structure;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueInhibitorDestroyedEvent : MutableLeagueKillerWithAssistersEvent, ILeagueInhibitorDestroyedEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.InhibitorDestroyed;

        public ILeagueInhibitor Inhibitor { get; set; } = NullLeagueInhibitor.Instance;
    }
}