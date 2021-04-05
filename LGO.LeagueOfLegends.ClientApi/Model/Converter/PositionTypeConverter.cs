using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class PositionTypeConverter : AbstractStringEnumConverter<LolPositionType>
    {
        public PositionTypeConverter() : base((LolPositionType.Undefined, "UNDEFINED"),
                                              (LolPositionType.Top, "TOP"),
                                              (LolPositionType.Jungle, "JUNGLE"),
                                              (LolPositionType.Middle, "MIDDLE"),
                                              (LolPositionType.Bottom, "BOTTOM"),
                                              (LolPositionType.Support, "UTILITY")) { }
    }
}