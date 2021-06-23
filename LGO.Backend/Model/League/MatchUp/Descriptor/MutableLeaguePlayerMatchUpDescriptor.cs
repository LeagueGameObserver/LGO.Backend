using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp.Descriptor
{
    internal sealed class MutableLeaguePlayerMatchUpDescriptor : MutableLeagueMatchUpDescriptor, ILeaguePlayerMatchUpDescriptor
    {
        public override LeagueMatchUpDescriptorType Type => LeagueMatchUpDescriptorType.Player;
        public LeaguePositionType Position { get; set; } = LeaguePositionType.Undefined;
    }
}