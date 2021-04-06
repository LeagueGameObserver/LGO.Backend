using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class GameResultConverter : AbstractStringEnumConverter<LolGameResult>
    {
        public GameResultConverter() : base((LolGameResult.Undefined, "UNDEFINED"),
            (LolGameResult.Win, "WIN"),
            (LolGameResult.Loss, "LOSE"))
        {
        }
    }
}