using LGO.Backend.Model.Event.Enum;
using LGO.Backend.Model.League;

namespace LGO.Backend.Model.Event
{
    public interface ILgoLeagueGameUpdatedEvent : ILgoEvent
    {
        LgoEventType ILgoEvent.Type => LgoEventType.LeagueGameUpdated;

        ILeagueGame Game { get; }
    }
}