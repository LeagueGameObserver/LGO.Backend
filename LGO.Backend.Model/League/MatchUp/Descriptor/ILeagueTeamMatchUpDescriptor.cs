namespace LGO.Backend.Model.League.MatchUp.Descriptor
{
    public interface ILeagueTeamMatchUpDescriptor : ILeagueMatchUpDescriptor
    {
        LeagueMatchUpDescriptorType ILeagueMatchUpDescriptor.Type => LeagueMatchUpDescriptorType.Team;
    }
}