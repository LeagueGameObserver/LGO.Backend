using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class TeamTypeConverter : AbstractStringEnumConverter<LolTeamType>
    {
        public TeamTypeConverter() : base((LolTeamType.Undefined, "UNDEFINED"),
                                          (LolTeamType.Blue, "ORDER"),
                                          (LolTeamType.Red, "CHAOS")) { }
    }
}