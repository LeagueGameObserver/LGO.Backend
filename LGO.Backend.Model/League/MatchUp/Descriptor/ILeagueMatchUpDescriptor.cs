using System;

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