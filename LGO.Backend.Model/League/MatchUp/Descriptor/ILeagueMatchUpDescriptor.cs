using System;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp.Descriptor
{
    public interface ILeagueMatchUpDescriptor
    {
        LeagueMatchUpDescriptorType Type { get; }
        
        Guid MatchUpId { get; }
        
        Guid BlueSideCompetitorId { get; }
        
        Guid RedSideCompetitorId { get; }
    }
}