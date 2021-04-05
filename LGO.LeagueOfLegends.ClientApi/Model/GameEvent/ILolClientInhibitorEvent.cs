using LGO.Backend.Core.Model.LeagueOfLegends.Structure;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientInhibitorEvent : ILolClientGameEvent
    {
        ILolInhibitor Inhibitor { get; }
    }
}