using LGO.Backend.Model.League.GameEvent;
using LGO.LeagueClient.Model.GameEvent;

namespace LGO.Backend.League
{
    internal static class LeagueGameEventConverter
    {
        public static bool TryConvert(ILeagueClientGameEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            switch (clientGameEvent)
            {
                case ILeagueClientGameStartedEvent gameStartedEvent:
                {
                    return TryConvert(gameStartedEvent, out gameEvent);
                }
                case ILeagueClientTurretDestroyedEvent turretDestroyedEvent:
                {
                    return TryConvert(turretDestroyedEvent, out gameEvent);
                }
                case ILeagueClientInhibitorDestroyedEvent inhibitorDestroyedEvent:
                {
                    return TryConvert(inhibitorDestroyedEvent, out gameEvent);
                }
                case ILeagueClientDragonKilledEvent dragonKilledEvent:
                {
                    return TryConvert(dragonKilledEvent, out gameEvent);
                }
                case ILeagueClientRiftHeraldKilledEvent riftHeraldKilledEvent:
                {
                    return TryConvert(riftHeraldKilledEvent, out gameEvent);
                }
                case ILeagueClientBaronNashorKilledEvent baronNashorKilledEvent:
                {
                    return TryConvert(baronNashorKilledEvent, out gameEvent);
                }
                case ILeagueClientChampionKilledEvent championKilledEvent:
                {
                    return TryConvert(championKilledEvent, out gameEvent);
                }
                case ILeagueClientMultipleChampionsKilledEvent multipleChampionsKilledEvent:
                {
                    return TryConvert(multipleChampionsKilledEvent, out gameEvent);
                }
                case ILeagueClientEntireTeamKilledEvent entireTeamKilledEvent:
                {
                    return TryConvert(entireTeamKilledEvent, out gameEvent);
                }
                case ILeagueClientGameEndedEvent gameEndedEvent:
                {
                    return TryConvert(gameEndedEvent, out gameEvent);
                }
                default:
                {
                    gameEvent = null!;
                    return false;
                }
            }
        }

        public static bool TryConvert(ILeagueClientGameStartedEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueGameStartedEvent {InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds};
            return true;
        }

        public static bool TryConvert(ILeagueClientTurretDestroyedEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueTurretDestroyedEvent
                        {
                            InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds,
                            KillerName = clientGameEvent.KillerName,
                            AssistersNames = clientGameEvent.AssistersNames,
                            Turret = clientGameEvent.Turret
                        };
            return true;
        }

        public static bool TryConvert(ILeagueClientInhibitorDestroyedEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueInhibitorDestroyedEvent
                        {
                            InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds,
                            KillerName = clientGameEvent.KillerName,
                            AssistersNames = clientGameEvent.AssistersNames,
                            Inhibitor = clientGameEvent.Inhibitor
                        };
            return true;
        }

        public static bool TryConvert(ILeagueClientDragonKilledEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueDragonKilledEvent
                        {
                            InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds,
                            KillerName = clientGameEvent.KillerName,
                            AssistersNames = clientGameEvent.AssistersNames,
                            Dragon = clientGameEvent.DragonType,
                            WasStolen = clientGameEvent.HasBeenStolen
                        };
            return true;
        }

        public static bool TryConvert(ILeagueClientRiftHeraldKilledEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueRiftHerladKilledEvent
                        {
                            InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds,
                            KillerName = clientGameEvent.KillerName,
                            AssistersNames = clientGameEvent.AssistersNames,
                            WasStolen = clientGameEvent.HasBeenStolen
                        };
            return true;
        }

        public static bool TryConvert(ILeagueClientBaronNashorKilledEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueBaronNashorKilledEvent
                        {
                            InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds,
                            KillerName = clientGameEvent.KillerName,
                            AssistersNames = clientGameEvent.AssistersNames,
                            WasStolen = clientGameEvent.HasBeenStolen
                        };
            return true;
        }

        public static bool TryConvert(ILeagueClientChampionKilledEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueChampionKilledEvent
                        {
                            InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds,
                            KillerName = clientGameEvent.KillerName,
                            AssistersNames = clientGameEvent.AssistersNames,
                            VictimSummonerName = clientGameEvent.VictimSummonerName
                        };
            return true;
        }

        public static bool TryConvert(ILeagueClientMultipleChampionsKilledEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueMultipleChampionsKilledEvent
                        {
                            InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds,
                            KillerName = clientGameEvent.KillerName,
                            NumberOfKills = clientGameEvent.NumberOfKills
                        };
            return true;
        }

        public static bool TryConvert(ILeagueClientEntireTeamKilledEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueTeamKilledEvent
                        {
                            InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds,
                            KillerName = clientGameEvent.KillerSummonerName
                        };
            return true;
        }

        public static bool TryConvert(ILeagueClientGameEndedEvent clientGameEvent, out ILeagueGameEvent gameEvent)
        {
            gameEvent = new MutableLeagueGameEndedEvent {InGameTimeInSeconds = clientGameEvent.InGameTime.TotalSeconds};
            return true;
        }
    }
}