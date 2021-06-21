using System;
using System.Collections.Generic;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model;

namespace LGO.Backend
{
    public sealed class DefaultLeagueGameConstantsFactory : ILeagueGameConstantsFactory
    {
        private Dictionary<LeagueMapType, Dictionary<LeagueGameModeType, ILeagueGameConstants>> GameConstants { get; } = new();

        public DefaultLeagueGameConstantsFactory()
        {
            InitializeSummonersRiftClassicConstants();
            InitializeHowlingAbyssAramConstants();
        }

        private void InitializeSummonersRiftClassicConstants()
        {
            var constants = new ImmutableGameConstants
                            {
                                InitialDragonSpawnTime = TimeSpan.FromSeconds(150.0d), // 2:30
                                DragonRespawnTime = TimeSpan.FromSeconds(300.0d), // 5:00   
                                ElderDragonBuffDuration = TimeSpan.FromSeconds(150.0d), // 2:30
                                InitialRiftHeraldSpawnTime = TimeSpan.FromSeconds(585.0d), // 9:45
                                RiftHeraldRespawnTime = TimeSpan.FromSeconds(360.0d), // 6:00
                                RiftHeraldDespawnTime = TimeSpan.FromSeconds(1185.0d), // 19:45
                                InitialBaronNashorSpawnTime = TimeSpan.FromSeconds(1200.0d), // 20:00
                                BaronNashorRespawnTime = TimeSpan.FromSeconds(360.0d), // 6:00
                                BaronNashorBuffDuration = TimeSpan.FromSeconds(180.0d), // 3:00
                                InhibitorRespawnTime = TimeSpan.FromSeconds(300.0d), // 5:00
                            };

            if (!GameConstants.TryGetValue(LeagueMapType.SummonersRift, out var modeConstants))
            {
                modeConstants = new Dictionary<LeagueGameModeType, ILeagueGameConstants>();
                GameConstants.Add(LeagueMapType.SummonersRift, modeConstants);
            }
            
            modeConstants.Add(LeagueGameModeType.Classic, constants);
        }

        private void InitializeHowlingAbyssAramConstants()
        {
            var constants = new ImmutableGameConstants
                            {
                                InhibitorRespawnTime = TimeSpan.FromSeconds(300.0d), // 5:00
                            };
            
            if (!GameConstants.TryGetValue(LeagueMapType.HowlingAbyss, out var modeConstants))
            {
                modeConstants = new Dictionary<LeagueGameModeType, ILeagueGameConstants>();
                GameConstants.Add(LeagueMapType.HowlingAbyss, modeConstants);
            }
            
            modeConstants.Add(LeagueGameModeType.Aram, constants);
        }

        public ILeagueGameConstants Of(LeagueMapType map, LeagueGameModeType gameMode, MultiComponentVersion gameVersion)
        {
            if (!GameConstants.TryGetValue(map, out var modeConstants))
            {
                throw new NotSupportedException($"The {nameof(LeagueMapType)} {map} is not yet supported. Sorry :/");
            }

            if (!modeConstants.TryGetValue(gameMode, out var constants))
            {
                throw new NotSupportedException($"The {nameof(LeagueGameModeType)} {gameMode} is not yet supported. Sorry :/");
            }

            return constants;
        }

        private class ImmutableGameConstants : ILeagueGameConstants
        {
            public TimeSpan? InitialDragonSpawnTime { get; init; }
            public TimeSpan? DragonRespawnTime { get; init; }
            public TimeSpan? ElderDragonBuffDuration { get; init; }
            public TimeSpan? InitialRiftHeraldSpawnTime { get; init; }
            public TimeSpan? RiftHeraldRespawnTime { get; init; }
            public TimeSpan? RiftHeraldDespawnTime { get; init; }
            public TimeSpan? InitialBaronNashorSpawnTime { get; init; }
            public TimeSpan? BaronNashorRespawnTime { get; init; }
            public TimeSpan? BaronNashorBuffDuration { get; init; }
            public TimeSpan? InhibitorRespawnTime { get; init; }
        }
    }
}