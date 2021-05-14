using System;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueClient.Model.Game;

namespace LGO.LeagueClient.LocalGameReader.Model.Game
{
    internal class NullGameStats : ILeagueClientGameStats
    {
        public TimeSpan InGameTime => TimeSpan.Zero;
        public LeagueGameModeType GameMode => LeagueGameModeType.Undefined;
        public LeagueMapType Map => LeagueMapType.Undefined;

        private static NullGameStats? _instance;

        public static NullGameStats Get => _instance ??= new NullGameStats();

        private NullGameStats() { }
    }
}