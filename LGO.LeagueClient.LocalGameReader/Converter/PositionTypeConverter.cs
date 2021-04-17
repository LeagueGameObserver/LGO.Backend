using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class PositionTypeConverter : ReadonlyStringEnumConverter<LeaguePositionType>
    {
        public PositionTypeConverter() : base(LeaguePositionType.Undefined,
                                              ("TOP", LeaguePositionType.Top),
                                              ("JUNGLE", LeaguePositionType.Jungle),
                                              ("MIDDLE", LeaguePositionType.Middle),
                                              ("BOTTOM", LeaguePositionType.Bottom),
                                              ("UTILITY", LeaguePositionType.Support))
        {
        }
    }
}