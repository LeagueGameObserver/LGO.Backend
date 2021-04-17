using LGO.Backend.Core.Model.League.Enum;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class GameModeTypeConverter : ReadonlyStringEnumConverter<LeagueGameModeType>
    {
        public GameModeTypeConverter() : base(LeagueGameModeType.Undefined,
                                              ("CLASSIC", LeagueGameModeType.Classic),
                                              ("ARAM", LeagueGameModeType.Aram),
                                              ("PRACTICETOOL", LeagueGameModeType.PracticeTool))
        {
        }
    }
}