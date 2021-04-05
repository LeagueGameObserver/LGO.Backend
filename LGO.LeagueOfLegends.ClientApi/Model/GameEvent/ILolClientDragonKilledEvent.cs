using LGO.Backend.Core.Model.LeagueOfLegends;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientDragonKilledEvent : ILolClientNeutralObjectiveKilledEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.DragonKilled;
        
        LolDragonType DragonType { get; }
    }
}