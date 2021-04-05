using System;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientGameEvent
    {
        int Id { get; }
        
        LolClientGameEventType Type { get; }
        
        TimeSpan InGameTime { get; }
    }
}