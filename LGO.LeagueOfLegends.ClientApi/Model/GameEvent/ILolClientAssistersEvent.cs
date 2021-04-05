using System.Collections.Generic;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientAssistersEvent : ILolClientGameEvent
    {
        IEnumerable<string> AssistersNames { get; }
    }
}