using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class MapConverter : ReadonlyStringEnumConverter<LeagueMapType>
    {
        public MapConverter() : base(LeagueMapType.Undefined,
                                     ("MAP11", LeagueMapType.SummonersRift),
                                     ("MAP12", LeagueMapType.HowlingAbyss))
        {
        }
    }
}