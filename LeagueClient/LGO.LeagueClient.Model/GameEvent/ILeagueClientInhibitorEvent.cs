using LGO.Backend.Core.Model.League.Structure;

namespace LGO.LeagueClient.Model.GameEvent
{
    public interface ILeagueClientInhibitorEvent : ILeagueClientGameEvent
    {
        ILeagueInhibitor Inhibitor { get; }
    }
}