using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.MatchUp.Descriptor
{
    internal sealed class MutableLeagueTeamMatchUpDescriptor : MutableLeagueMatchUpDescriptor, ILeagueTeamMatchUpDescriptor
    {
        public override LeagueMatchUpDescriptorType Type => LeagueMatchUpDescriptorType.Team;
    }
}