using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class GameResultConverter : ReadonlyStringEnumConverter<LeagueGameResult>
    {
        public GameResultConverter() : base(LeagueGameResult.Undefined,
                                            ("WIN", LeagueGameResult.Win),
                                            ("LOSE", LeagueGameResult.Loss))
        {
        }
    }
}