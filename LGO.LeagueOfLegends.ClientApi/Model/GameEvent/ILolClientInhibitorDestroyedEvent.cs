using LGO.Backend.Core.Model.LeagueOfLegends.Structure;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientInhibitorDestroyedEvent : ILolClientKillerEvent, ILolClientAssistersEvent, ILolClientInhibitorEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.InhibitorDestroyed;
    }
}