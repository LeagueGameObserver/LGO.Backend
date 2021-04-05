using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class GameModeTypeConverter : AbstractStringEnumConverter<LolGameModeType>
    {
        public GameModeTypeConverter() : base((LolGameModeType.Undefined, "UNDEFINED"),
                                              (LolGameModeType.Classic, "CLASSIC"),
                                              (LolGameModeType.Aram, "ARAM"),
                                              (LolGameModeType.PracticeTool, "PRACTICETOOL")) { }
    }
}