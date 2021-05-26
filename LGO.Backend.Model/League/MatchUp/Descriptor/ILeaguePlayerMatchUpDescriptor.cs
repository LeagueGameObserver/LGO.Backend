using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp.Descriptor
{
    public interface ILeaguePlayerMatchUpDescriptor : ILeagueMatchUpDescriptor
    {
        LeagueMatchUpDescriptorType ILeagueMatchUpDescriptor.Type => LeagueMatchUpDescriptorType.Player;

        LeaguePositionType Position { get; }
    }
}