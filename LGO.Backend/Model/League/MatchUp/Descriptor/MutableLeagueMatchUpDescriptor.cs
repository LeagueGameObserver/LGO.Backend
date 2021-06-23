using System;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp.Descriptor
{
    internal abstract class MutableLeagueMatchUpDescriptor : ILeagueMatchUpDescriptor
    {
        public abstract LeagueMatchUpDescriptorType Type { get; }
        public Guid MatchUpId { get; set; } = Guid.Empty;
        public Guid BlueSideCompetitorId { get; set; } = Guid.Empty;
        public Guid RedSideCompetitorId { get; set; } = Guid.Empty;
    }
}