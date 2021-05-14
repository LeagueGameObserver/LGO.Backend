using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Core.Utility;
using LGO.Backend.League.Snapshot;
using LGO.Backend.League.Snapshot.Timer;
using LGO.Backend.Model.League.Enum;
using LGO.Backend.Model.League.GameEvent;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueClient.Model.GameEvent;
using LGO.LeagueClient.Model.Player;
using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Item;

namespace LGO.Backend.League
{
    internal sealed class LgoLeagueGame
    {
        public event EventHandler<InternalLeagueGameSnapshot>? NewSnapshotAdded;

        public Guid Id { get; } = Guid.NewGuid();

        public InternalLeagueGameSnapshot CurrentSnapshot
        {
            get
            {
                lock (Lock)
                {
                    return Snapshots[^1];
                }
            }
        }

        private object Lock { get; } = new();
        private ILeagueResourceRepository ResourceRepository { get; }
        private ILeagueGameConstants GameConstants { get; }
        private List<InternalLeagueGameSnapshot> Snapshots { get; } = new();
        private InternalLeagueGameSnapshot? _previousGameSnapshot;
        private ILeagueClientGame _currentClientGame = null!;

        public LgoLeagueGame(ILeagueResourceRepository resourceRepository, ILeagueGameConstants gameConstants, ILeagueClientGame clientGame)
        {
            ResourceRepository = resourceRepository;
            GameConstants = gameConstants;
            Update(clientGame);
        }

        public void Update(ILeagueClientGame clientGame)
        {
            lock (Lock)
            {
                Snapshots.RemoveAll(s => s.InGameTime >= clientGame.Stats.InGameTime);

                _previousGameSnapshot = Snapshots.LastOrDefault();
                _currentClientGame = clientGame;

                var newClientGameEvents = ExtractNewGameEvents();
                var newInternalItemChangedEvents = ExtractNewItemChangedEvents();
                var playerSnapshots = ExtractPlayerSnapshots(newInternalItemChangedEvents).Result;
                var newItemChangedEvents = ConvertItemChangedEvents(newInternalItemChangedEvents, playerSnapshots);

                var newGameEvents = new List<ILeagueGameEvent>();
                newGameEvents.AddRange(newClientGameEvents);
                newGameEvents.AddRange(newItemChangedEvents);

                var teamSnapshots = ExtractTeamSnapshots(newGameEvents, playerSnapshots);
                var timerSnapshots = ExtractTimerSnapshots(newGameEvents);

                var allGameEvents = (_previousGameSnapshot?.Events ?? Enumerable.Empty<ILeagueGameEvent>()).Concat(newGameEvents);

                var gameState = LeagueGameStateType.Loading;
                if (_currentClientGame.EventCollection.Events.Any(e => e.Type == LeagueClientGameEventType.GameEnded))
                {
                    gameState = LeagueGameStateType.Ended;
                }
                else if (_currentClientGame.EventCollection.Events.Any(e => e.Type == LeagueClientGameEventType.GameStarted))
                {
                    gameState = LeagueGameStateType.InProgress;
                }

                var snapshot = new InternalLeagueGameSnapshot
                               {
                                   State = gameState,
                                   Mode = clientGame.Stats.GameMode,
                                   InGameTime = clientGame.Stats.InGameTime,
                                   Teams = teamSnapshots,
                                   Players = playerSnapshots,
                                   Timers = timerSnapshots,
                                   Events = allGameEvents
                               };
                Snapshots.Add(snapshot);

                NewSnapshotAdded?.Invoke(this, snapshot);
            }
        }

        private IReadOnlyCollection<ILeagueGameEvent> ExtractNewGameEvents()
        {
            var result = new List<ILeagueGameEvent>();

            var previousGameTime = _previousGameSnapshot?.InGameTime ?? TimeSpan.Zero;
            foreach (var newClientGameEvent in _currentClientGame.EventCollection.Events.Where(e => e.InGameTime > previousGameTime))
            {
                if (LeagueGameEventConverter.TryConvert(newClientGameEvent, out var gameEvent))
                {
                    result.Add(gameEvent);
                }
            }

            return result;
        }

        private IReadOnlyCollection<InternalItemChangedEvent> ExtractNewItemChangedEvents()
        {
            var result = new List<InternalItemChangedEvent>();

            foreach (var clientPlayer in _currentClientGame.Players)
            {
                var previousItemIds = (_previousGameSnapshot?.Players
                                                             .FirstOrDefault(p => PlayersAreEqual(p, clientPlayer))?
                                                             .Items
                                                             .SelectMany(i => Enumerable.Repeat(i.Id, i.Amount)) ?? Enumerable.Empty<int>()).ToList();
                var currentItemIds = clientPlayer.Items.SelectMany(i => Enumerable.Repeat(i.Id, i.Amount)).ToList();

                var addedItemIds = MultiSet.Difference(currentItemIds, previousItemIds).ToList();
                var removedItemIds = MultiSet.Difference(previousItemIds, currentItemIds).ToList();

                if (addedItemIds.Count < 1 && removedItemIds.Count < 1)
                {
                    continue;
                }

                result.Add(new InternalItemChangedEvent
                           {
                               SummonerName = clientPlayer.SummonerName,
                               SummonerTeam = clientPlayer.Team,
                               AddedItemIds = addedItemIds,
                               RemovedItemIds = removedItemIds
                           });
            }

            return result;
        }

        private async Task<IReadOnlyCollection<InternalLeaguePlayerSnapshot>> ExtractPlayerSnapshots(IReadOnlyCollection<InternalItemChangedEvent> itemChangedEvents)
        {
            var result = new List<InternalLeaguePlayerSnapshot>(_currentClientGame.Players.Count());

            foreach (var clientPlayer in _currentClientGame.Players)
            {
                var previousPlayerSnapshot = _previousGameSnapshot?.Players.FirstOrDefault(p => PlayersAreEqual(p, clientPlayer));
                var itemSnapshots = clientPlayer.Items.Select(i => new InternalLeagueItemSnapshot
                                                                   {
                                                                       Id = i.Id,
                                                                       Amount = i.Amount
                                                                   }).ToList();

                var totalItemWorth = 0;
                foreach (var item in itemSnapshots)
                {
                    var resourceItem = await ResourceRepository.ReadItemByKeyAsync(item.Id);
                    if (resourceItem == null)
                    {
                        throw new Exception($"Unable to retrieve {nameof(ILeagueResourceItem)} with id {item.Id}.");
                    }

                    totalItemWorth += resourceItem.Costs.TotalCosts * item.Amount;
                }

                var unspentKills = (previousPlayerSnapshot?.UnspentKills ?? 0) + clientPlayer.Score.Kills - (previousPlayerSnapshot?.TotalKills ?? 0);
                var unspentAssists = (previousPlayerSnapshot?.UnspentAssists ?? 0) + clientPlayer.Score.Assists - (previousPlayerSnapshot?.TotalAssists ?? 0);
                var unspentDeaths = (previousPlayerSnapshot?.UnspentDeaths ?? 0) + clientPlayer.Score.Deaths - (previousPlayerSnapshot?.TotalDeaths ?? 0);

                if (itemChangedEvents.Any(e => e.SummonerName.Equals(clientPlayer.SummonerName) && e.SummonerTeam == clientPlayer.Team))
                {
                    unspentKills = 0;
                    unspentAssists = 0;
                    unspentDeaths = 0;
                }

                result.Add(new InternalLeaguePlayerSnapshot
                           {
                               Id = previousPlayerSnapshot?.Id ?? Guid.NewGuid(),
                               Items = itemSnapshots,
                               Team = clientPlayer.Team,
                               ChampionName = clientPlayer.ChampionName,
                               SummonerName = clientPlayer.SummonerName,
                               IsDead = clientPlayer.IsDead,
                               RespawnTime = clientPlayer.RespawnTime,
                               TotalAssists = clientPlayer.Score.Assists,
                               TotalDeaths = clientPlayer.Score.Deaths,
                               TotalKills = clientPlayer.Score.Kills,
                               UnspentAssists = unspentAssists,
                               UnspentDeaths = unspentDeaths,
                               UnspentKills = unspentKills,
                               TotalGoldOwned = totalItemWorth,
                           });
            }

            return result;
        }

        private IReadOnlyCollection<ILeagueItemsChangedEvent> ConvertItemChangedEvents(IReadOnlyCollection<InternalItemChangedEvent> internalItemChangedEvents,
                                                                                       IReadOnlyCollection<InternalLeaguePlayerSnapshot> playerSnapshots)
        {
            var result = new List<ILeagueItemsChangedEvent>(internalItemChangedEvents.Count);

            foreach (var internalEvent in internalItemChangedEvents)
            {
                var player = playerSnapshots.FirstOrDefault(p => p.SummonerName.Equals(internalEvent.SummonerName) && p.Team == internalEvent.SummonerTeam);
                if (player == null)
                {
                    throw new Exception($"Unable to find the {nameof(InternalLeaguePlayerSnapshot)} corresponding to the given {nameof(InternalItemChangedEvent)}.");
                }

                result.Add(new MutableLeagueItemsChangedEvent
                           {
                               SummonerName = player.SummonerName,
                               InGameTimeInSeconds = _currentClientGame.Stats.InGameTime.TotalSeconds,
                               AddedItemIds = internalEvent.AddedItemIds,
                               RemovedItemIds = internalEvent.RemovedItemIds
                           });
            }

            return result;
        }

        private IReadOnlyCollection<InternalLeagueTeamSnapshot> ExtractTeamSnapshots(IReadOnlyCollection<ILeagueGameEvent> newGamEvents,
                                                                                     IReadOnlyCollection<InternalLeaguePlayerSnapshot> playerSnapshots)
        {
            var result = new List<InternalLeagueTeamSnapshot>();

            foreach (var teamSide in playerSnapshots.Select(p => p.Team).Distinct())
            {
                var enemyTeamSide = teamSide switch
                                    {
                                        LeagueTeamType.Blue => LeagueTeamType.Red,
                                        LeagueTeamType.Red => LeagueTeamType.Blue,
                                        _ => throw new Exception($"Unable to determine the enemy team of {teamSide}.")
                                    };

                var previousTeamSnapshot = _previousGameSnapshot?.Teams.FirstOrDefault(t => t.Side == teamSide);
                var teamPlayers = playerSnapshots.Where(p => p.Team == teamSide).ToList();
                var teamPlayerSummonerNames = teamPlayers.Select(p => p.SummonerName).ToList();

                var dragonsKilled = (previousTeamSnapshot?.DragonsKilled ?? Enumerable.Empty<LeagueDragonType>()).ToList();
                var turretsDestroyed = (previousTeamSnapshot?.TurretsDestroyed ?? Enumerable.Empty<LeagueTurretTierType>()).ToList();
                var inhibitorsDestroyed = (previousTeamSnapshot?.InhibitorsDestroyed ?? Enumerable.Empty<LeagueInhibitorTierType>()).ToList();
                var numberOfRiftHeraldsKilled = previousTeamSnapshot?.NumberOfRiftHeraldsKilled ?? 0;
                var numberOfBaronNashorsKilled = previousTeamSnapshot?.NumberOfBaronNashorsKilled ?? 0;

                foreach (var gameEvent in newGamEvents)
                {
                    if (gameEvent is ILeagueDragonKilledEvent dragonKilledEvent && teamPlayerSummonerNames.Contains(dragonKilledEvent.KillerName))
                    {
                        dragonsKilled.Add(dragonKilledEvent.Dragon);
                        continue;
                    }

                    if (gameEvent is ILeagueTurretDestroyedEvent turretDestroyedEvent && turretDestroyedEvent.Turret.Team == enemyTeamSide)
                    {
                        turretsDestroyed.Add(turretDestroyedEvent.Turret.Tier);
                        continue;
                    }

                    if (gameEvent is ILeagueInhibitorDestroyedEvent inhibitorDestroyedEvent && inhibitorDestroyedEvent.Inhibitor.Team == enemyTeamSide)
                    {
                        inhibitorsDestroyed.Add(inhibitorDestroyedEvent.Inhibitor.Tier);
                        continue;
                    }

                    if (gameEvent is ILeagueRiftHeraldKilledEvent riftHeraldKilledEvent && teamPlayerSummonerNames.Contains(riftHeraldKilledEvent.KillerName))
                    {
                        numberOfRiftHeraldsKilled++;
                        continue;
                    }

                    if (gameEvent is ILeagueBaronNashorKilledEvent baronNashorKilledEvent && teamPlayerSummonerNames.Contains(baronNashorKilledEvent.KillerName))
                    {
                        numberOfBaronNashorsKilled++;
                        continue;
                    }
                }

                result.Add(new InternalLeagueTeamSnapshot
                           {
                               Id = previousTeamSnapshot?.Id ?? Guid.NewGuid(),
                               Side = teamSide,
                               DragonsKilled = dragonsKilled,
                               TurretsDestroyed = turretsDestroyed,
                               InhibitorsDestroyed = inhibitorsDestroyed,
                               NumberOfRiftHeraldsKilled = numberOfRiftHeraldsKilled,
                               NumberOfBaronNashorsKilled = numberOfBaronNashorsKilled,
                               TotalAssists = teamPlayers.Sum(p => p.TotalAssists),
                               TotalDeaths = teamPlayers.Sum(p => p.TotalDeaths),
                               TotalKills = teamPlayers.Sum(p => p.TotalKills),
                               UnspentAssists = teamPlayers.Sum(p => p.UnspentAssists),
                               UnspentDeaths = teamPlayers.Sum(p => p.UnspentDeaths),
                               UnspentKills = teamPlayers.Sum(p => p.UnspentKills),
                               TotalGoldOwned = teamPlayers.Sum(p => p.TotalGoldOwned)
                           });
            }

            return result;
        }

        private IReadOnlyCollection<InternalLeagueTimerSnapshot> ExtractTimerSnapshots(IReadOnlyCollection<ILeagueGameEvent> newGameEvents)
        {
            var currentGameTime = _currentClientGame.Stats.InGameTime;
            var previousTimers = _previousGameSnapshot?.Timers ?? Enumerable.Empty<InternalLeagueTimerSnapshot>();
            var playersThatDiedSinceLastUpdate = newGameEvents.Where(e => e is ILeagueChampionKilledEvent)
                                                              .Cast<ILeagueChampionKilledEvent>()
                                                              .Select(e => e.VictimSummonerName);

            var result = new List<InternalLeagueTimerSnapshot>();
            foreach (var timer in previousTimers.Where(t => t.InGameEndTime > currentGameTime))
            {
                if (timer is InternalLeagueInhibitorRespawnTimerSnapshot inhibitorRespawnTimer)
                {
                    result.Add(new InternalLeagueInhibitorRespawnTimerSnapshot
                               {
                                   Id = inhibitorRespawnTimer.Id,
                                   Type = inhibitorRespawnTimer.Type,
                                   Inhibitor = inhibitorRespawnTimer.Inhibitor,
                                   InGameStartTime = timer.InGameStartTime,
                                   InGameEndTime = timer.InGameEndTime,
                                   RemainingTime = timer.InGameEndTime - currentGameTime
                               });
                    continue;
                }

                if (timer is InternalLeagueNeutralObjectiveRespawnTimerSnapshot neutralObjectiveRespawnTimer)
                {
                    result.Add(new InternalLeagueNeutralObjectiveRespawnTimerSnapshot
                               {
                                   Id = neutralObjectiveRespawnTimer.Id,
                                   Type = neutralObjectiveRespawnTimer.Type,
                                   InGameStartTime = timer.InGameStartTime,
                                   InGameEndTime = timer.InGameEndTime,
                                   RemainingTime = timer.InGameEndTime - currentGameTime
                               });
                    continue;
                }

                if (timer is InternalLeaguePowerPlayTimerSnapshot powerPlayTimer)
                {
                    var buffedPlayers = powerPlayTimer.BuffedPlayers.ToList();
                    buffedPlayers.RemoveAll(p => playersThatDiedSinceLastUpdate.Contains(p));

                    result.Add(new InternalLeaguePowerPlayTimerSnapshot
                               {
                                   Id = powerPlayTimer.Id,
                                   Type = powerPlayTimer.Type,
                                   Team = powerPlayTimer.Team,
                                   BuffedPlayers = buffedPlayers,
                                   InGameStartTime = timer.InGameStartTime,
                                   InGameEndTime = timer.InGameEndTime,
                                   RemainingTime = timer.InGameEndTime - currentGameTime
                               });
                    continue;
                }
            }

            foreach (var gameEvent in newGameEvents)
            {
                var startTime = TimeSpan.FromSeconds(gameEvent.InGameTimeInSeconds);

                if (gameEvent is ILeagueInhibitorDestroyedEvent inhibitorDestroyedEvent && GameConstants.InhibitorRespawnTime.HasValue)
                {
                    var endTime = startTime + GameConstants.InhibitorRespawnTime.Value;
                    result.Add(new InternalLeagueInhibitorRespawnTimerSnapshot
                               {
                                   Id = Guid.NewGuid(),
                                   Type = LeagueTimerType.InhibitorRespawn,
                                   Inhibitor = inhibitorDestroyedEvent.Inhibitor,
                                   InGameStartTime = startTime,
                                   InGameEndTime = endTime,
                                   RemainingTime = endTime - currentGameTime
                               });
                    continue;
                }

                if (gameEvent is ILeagueRiftHeraldKilledEvent && GameConstants.RiftHeraldRespawnTime.HasValue)
                {
                    var endTime = startTime + GameConstants.RiftHeraldRespawnTime.Value;
                    if (GameConstants.RiftHeraldDespawnTime.HasValue && endTime >= GameConstants.RiftHeraldDespawnTime.Value)
                    {
                        continue;
                    }

                    result.Add(new InternalLeagueNeutralObjectiveRespawnTimerSnapshot
                               {
                                   Id = Guid.NewGuid(),
                                   Type = LeagueTimerType.RiftHeraldRespawn,
                                   InGameStartTime = startTime,
                                   InGameEndTime = endTime,
                                   RemainingTime = endTime - currentGameTime
                               });
                    continue;
                }

                if (gameEvent is ILeagueDragonKilledEvent dragonKilledEvent && GameConstants.DragonRespawnTime.HasValue)
                {
                    var endTime = startTime + GameConstants.DragonRespawnTime.Value;
                    result.Add(new InternalLeagueNeutralObjectiveRespawnTimerSnapshot
                               {
                                   Id = Guid.NewGuid(),
                                   Type = LeagueTimerType.DragonRespawn,
                                   InGameStartTime = startTime,
                                   InGameEndTime = endTime,
                                   RemainingTime = endTime - currentGameTime
                               });

                    if (dragonKilledEvent.Dragon == LeagueDragonType.Elder && GameConstants.ElderDragonBuffDuration.HasValue)
                    {
                        result.Add(CreatePowerPlayTimer(LeagueTimerType.ElderDragonPowerPlay, dragonKilledEvent, GameConstants.ElderDragonBuffDuration.Value));
                    }

                    continue;
                }

                if (gameEvent is ILeagueBaronNashorKilledEvent baronNashorKilledEvent && GameConstants.BaronNashorRespawnTime.HasValue)
                {
                    var endTime = startTime + GameConstants.BaronNashorRespawnTime.Value;
                    result.Add(new InternalLeagueNeutralObjectiveRespawnTimerSnapshot
                               {
                                   Id = Guid.NewGuid(),
                                   Type = LeagueTimerType.BaronNashorRespawn,
                                   InGameStartTime = startTime,
                                   InGameEndTime = endTime,
                                   RemainingTime = endTime - currentGameTime
                               });

                    if (GameConstants.BaronNashorBuffDuration.HasValue)
                    {
                        result.Add(CreatePowerPlayTimer(LeagueTimerType.BaronNashorPowerPlay, baronNashorKilledEvent, GameConstants.BaronNashorBuffDuration.Value));
                    }

                    continue;
                }
            }

            if (_previousGameSnapshot == null)
            {
                // create initial spawn timers
                if (GameConstants.InitialDragonSpawnTime.HasValue)
                {
                    result.Add(CreateInitialNeutralObjectiveSpawnTimer(LeagueTimerType.DragonRespawn, GameConstants.InitialDragonSpawnTime.Value));
                }

                if (GameConstants.InitialRiftHeraldSpawnTime.HasValue)
                {
                    result.Add(CreateInitialNeutralObjectiveSpawnTimer(LeagueTimerType.RiftHeraldRespawn, GameConstants.InitialRiftHeraldSpawnTime.Value));
                }

                if (GameConstants.InitialBaronNashorSpawnTime.HasValue)
                {
                    result.Add(CreateInitialNeutralObjectiveSpawnTimer(LeagueTimerType.BaronNashorRespawn, GameConstants.InitialBaronNashorSpawnTime.Value));
                }
            }

            return result.Where(e => e.InGameEndTime > currentGameTime).ToList();
        }

        private InternalLeaguePowerPlayTimerSnapshot CreatePowerPlayTimer(LeagueTimerType type, ILeagueKillerEvent neutralObjectiveKilledEvent, TimeSpan buffDuration)
        {
            var startTime = TimeSpan.FromSeconds(neutralObjectiveKilledEvent.InGameTimeInSeconds);
            var buffEndTime = startTime + buffDuration;
            var killer = _currentClientGame.Players.FirstOrDefault(p => p.SummonerName.Equals(neutralObjectiveKilledEvent.KillerName));
            if (killer == null)
            {
                throw new Exception($"Unable to find {nameof(InternalLeaguePlayerSnapshot)} with summoner name {neutralObjectiveKilledEvent.KillerName}.");
            }

            return new InternalLeaguePowerPlayTimerSnapshot
                   {
                       Id = Guid.NewGuid(),
                       Type = type,
                       Team = killer.Team,
                       BuffedPlayers = GetBuffedPlayerSummonerNames(neutralObjectiveKilledEvent, killer, buffDuration),
                       InGameStartTime = startTime,
                       InGameEndTime = buffEndTime,
                       RemainingTime = buffEndTime - _currentClientGame.Stats.InGameTime
                   };
        }

        private IEnumerable<string> GetBuffedPlayerSummonerNames(ILeagueKillerEvent killedEvent, ILeagueClientPlayer currentKillerSnapshot, TimeSpan buffDuration)
        {
            var buffedPlayers = new List<string>();

            var buffStartTime = TimeSpan.FromSeconds(killedEvent.InGameTimeInSeconds);
            var buffEndTime = buffStartTime + buffDuration;

            foreach (var teamPlayer in _currentClientGame.Players.Where(p => p.Team == currentKillerSnapshot.Team))
            {
                var currentPlayer = _currentClientGame.Players.FirstOrDefault(p => p.SummonerName.Equals(teamPlayer.SummonerName) && p.Team == currentKillerSnapshot.Team);
                if (currentPlayer == null)
                {
                    throw new Exception($"Unable to find current {nameof(ILeagueClientPlayer)} with summoner name {teamPlayer.SummonerName}.");
                }

                if (_currentClientGame.EventCollection.Events
                                      .Where(e => e is ILeagueClientChampionKilledEvent)
                                      .Cast<ILeagueClientChampionKilledEvent>()
                                      .Where(e => e.VictimSummonerName.Equals(currentPlayer.SummonerName))
                                      .Any(e => e.InGameTime >= buffStartTime && e.InGameTime < buffEndTime))
                {
                    // player died at least once after the buff was acquired
                    continue;
                }

                if (_previousGameSnapshot == null)
                {
                    buffedPlayers.Add(teamPlayer.SummonerName);
                    continue;
                }

                var previousPlayer = _previousGameSnapshot.Players.FirstOrDefault(p => p.SummonerName.Equals(teamPlayer.SummonerName) && p.Team == currentKillerSnapshot.Team);
                if (previousPlayer == null)
                {
                    throw new Exception($"Unable to find previous {nameof(ILeagueClientPlayer)} with summoner name {teamPlayer.SummonerName}.");
                }

                if (previousPlayer.IsDead && _previousGameSnapshot.InGameTime + previousPlayer.RespawnTime > TimeSpan.FromSeconds(killedEvent.InGameTimeInSeconds))
                {
                    // player was dead when the buff was acquired
                    continue;
                }

                buffedPlayers.Add(teamPlayer.SummonerName);
            }

            return buffedPlayers;
        }

        private InternalLeagueNeutralObjectiveRespawnTimerSnapshot CreateInitialNeutralObjectiveSpawnTimer(LeagueTimerType type, TimeSpan initialSpawnTime)
        {
            var currentGameTime = _currentClientGame.Stats.InGameTime;
            return new InternalLeagueNeutralObjectiveRespawnTimerSnapshot
                   {
                       Id = Guid.NewGuid(),
                       Type = type,
                       InGameStartTime = TimeSpan.Zero,
                       InGameEndTime = initialSpawnTime,
                       RemainingTime = initialSpawnTime - currentGameTime
                   };
        }

        private static bool PlayersAreEqual(InternalLeaguePlayerSnapshot playerSnapshot, ILeagueClientPlayer clientPlayer)
        {
            return playerSnapshot.SummonerName.Equals(clientPlayer.SummonerName) && playerSnapshot.Team == clientPlayer.Team;
        }

        private sealed record InternalItemChangedEvent
        {
            public string SummonerName { get; init; } = null!;
            public LeagueTeamType SummonerTeam { get; init; }
            public IEnumerable<int> AddedItemIds { get; init; } = null!;
            public IEnumerable<int> RemovedItemIds { get; init; } = null!;
        }
    }
}