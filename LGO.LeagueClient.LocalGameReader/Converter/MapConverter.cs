using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class MapConverter : ReadonlyStringEnumConverter<LeagueMap>
    {
        public MapConverter() : base(LeagueMap.Undefined,
                                     ("MAP11", LeagueMap.SummonersRift),
                                     ("MAP12", LeagueMap.HowlingAbyss))
        {
        }
    }
}