using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class TeamTypeConverter : ReadonlyStringEnumConverter<LeagueTeamType>
    {
        public TeamTypeConverter() : base(LeagueTeamType.Undefined,
                                          ("ORDER", LeagueTeamType.Blue),
                                          ("CHAOS", LeagueTeamType.Red))
        {
        }
    }
}