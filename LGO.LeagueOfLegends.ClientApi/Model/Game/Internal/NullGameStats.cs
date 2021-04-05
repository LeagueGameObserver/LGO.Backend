using System;
using LGO.Backend.Core.Model.LeagueOfLegends.Enum;

namespace LGO.LeagueOfLegends.ClientApi.Model.Game.Internal
{
    internal class NullGameStats : ILolClientGameStats
    {
        public TimeSpan InGameTime => TimeSpan.Zero;
        public LolGameModeType GameMode => LolGameModeType.Undefined;

        private static NullGameStats? _instance;

        public static NullGameStats Get => _instance ??= new NullGameStats();

        private NullGameStats() { }
    }
}