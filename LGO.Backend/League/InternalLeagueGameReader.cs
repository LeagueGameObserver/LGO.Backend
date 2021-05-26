using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.League.Snapshot;
using LGO.Backend.League.Snapshot.Timer;
using LGO.Backend.Model;
using LGO.Backend.Model.League;
using LGO.Backend.Model.League.Enum;
using LGO.Backend.Model.League.MatchUp;
using LGO.Backend.Model.League.MatchUp.Descriptor;
using LGO.Backend.Model.League.Player;
using LGO.Backend.Model.League.Team;
using LGO.Backend.Model.League.Timer;
using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Item;

namespace LGO.Backend.League
{
    internal sealed class InternalLeagueGameReader : ILeagueGameReader
    {
        public event EventHandler? GameUpdated;
        
        public Guid GameId => Game.Id;

        public IEnumerable<ILeagueMatchUpDescriptor> MatchUpDescriptors => MutableMatchUpDescriptors.Values;

        private InternalLeagueGame Game { get; }
        private ILeagueResourceRepository ResourceRepository { get; }
        private ConcurrentDictionary<Guid, ILeagueMatchUpDescriptor> MutableMatchUpDescriptors { get; } = new();

        private int _numberOfEventsAtLastRead = 0;

        public InternalLeagueGameReader(InternalLeagueGame game, ILeagueResourceRepository resourceRepository)
        {
            Game = game;
            Game.NewSnapshotAdded += Game_OnNewSnapshotAdded;
            ResourceRepository = resourceRepository;
            
            foreach (var initialMatchUpDescriptor in game.InitialMatchUpDescriptors)
            {
                TryAddMatchUpDescriptor(initialMatchUpDescriptor);
            }
        }

        private void Game_OnNewSnapshotAdded(object? sender, InternalLeagueGameSnapshot e)
        {
            GameUpdated?.Invoke(this, EventArgs.Empty);
        }

        public bool TryRemoveMatchUpDescriptor(Guid matchUpId)
        {
            if (!MutableMatchUpDescriptors.TryRemove(matchUpId, out _))
            {
                return false;
            }

            return true;
        }

        public bool TryAddMatchUpDescriptor(ILeagueMatchUpDescriptor matchUpDescriptor)
        {
            if (!IsValidMatchUp(matchUpDescriptor))
            {
                return false;
            }

            if (!MutableMatchUpDescriptors.TryAdd(matchUpDescriptor.MatchUpId, matchUpDescriptor))
            {
                return false;
            }

            return true;
        }

        public bool AddOrReplaceMatchUpDescriptor(ILeagueMatchUpDescriptor matchUpDescriptor)
        {
            if (!IsValidMatchUp(matchUpDescriptor))
            {
                return false;
            }

            MutableMatchUpDescriptors.AddOrUpdate(matchUpDescriptor.MatchUpId, _ => matchUpDescriptor, (_, _) => matchUpDescriptor);
            return true;
        }

        private bool IsValidMatchUp(ILeagueMatchUpDescriptor matchUpDescriptor)
        {
            var gameSnapshot = Game.CurrentSnapshot;
            if (IsPlayerMatchUp(matchUpDescriptor, gameSnapshot))
            {
                return true;
            }

            if (IsTeamMatchUp(matchUpDescriptor, gameSnapshot))
            {
                return true;
            }

            return false;
        }

        private bool IsPlayerMatchUp(ILeagueMatchUpDescriptor matchUpDescriptor, InternalLeagueGameSnapshot gameSnapshot)
        {
            if (matchUpDescriptor.Type != LeagueMatchUpDescriptorType.Player)
            {
                return false;
            }

            var competitorBlueSide = gameSnapshot.Players.FirstOrDefault(p => p.Team == LeagueTeamType.Blue && p.Id.Equals(matchUpDescriptor.BlueSideCompetitorId));
            var competitorRedSide = gameSnapshot.Players.FirstOrDefault(p => p.Team == LeagueTeamType.Red && p.Id.Equals(matchUpDescriptor.RedSideCompetitorId));

            return competitorBlueSide != null && competitorRedSide != null;
        }

        private bool IsTeamMatchUp(ILeagueMatchUpDescriptor matchUpDescriptor, InternalLeagueGameSnapshot gameSnapshot)
        {
            if (matchUpDescriptor.Type != LeagueMatchUpDescriptorType.Team)
            {
                return false;
            }

            var competitorBlueSide = gameSnapshot.Teams.FirstOrDefault(t => t.Side == LeagueTeamType.Blue && t.Id.Equals(matchUpDescriptor.BlueSideCompetitorId));
            var competitorRedSide = gameSnapshot.Teams.FirstOrDefault(t => t.Side == LeagueTeamType.Red && t.Id.Equals(matchUpDescriptor.RedSideCompetitorId));

            return competitorBlueSide != null && competitorRedSide != null;
        }

        public async Task<ILeagueGame> ReadGameAsync(ILgoClientSettings clientSettings)
        {
            var gameSnapshot = Game.CurrentSnapshot;
            var players = await ExtractPlayers(gameSnapshot, clientSettings);
            var teams = ExtractTeams(gameSnapshot, clientSettings);
            var matchUps = ExtractMatchUps(gameSnapshot);
            var timers = ExtractTimers(gameSnapshot, clientSettings);
            var events = gameSnapshot.Events;
            var eventsSinceLastUpdate = gameSnapshot.Events.Skip(_numberOfEventsAtLastRead);
            _numberOfEventsAtLastRead = gameSnapshot.Events.Count();

            return new MutableLeagueGame
                   {
                       Id = Game.Id,
                       State = gameSnapshot.State,
                       Mode = gameSnapshot.Mode,
                       InGameTimeInSeconds = gameSnapshot.InGameTime.TotalSeconds,
                       Teams = teams,
                       Players = players,
                       MatchUps = matchUps,
                       Timers = timers,
                       Events = events,
                       EventsSinceLastUpdate = eventsSinceLastUpdate
                   };
        }

        private async Task<IEnumerable<ILeaguePlayer>> ExtractPlayers(InternalLeagueGameSnapshot snapshot, ILgoClientSettings clientSettings)
        {
            var players = new List<ILeaguePlayer>();
            var retrievalConfiguration = clientSettings.LeaguePlayerRetrievalConfiguration;

            foreach (var playerSnapshot in snapshot.Players)
            {
                var mutablePlayer = new MutableLeaguePlayer
                                    {
                                        Id = playerSnapshot.Id,
                                        TotalGoldOwned = playerSnapshot.TotalGoldOwned,
                                        UnspentKills = playerSnapshot.UnspentKills,
                                        UnspentAssists = playerSnapshot.UnspentAssists,
                                        UnspentDeaths = playerSnapshot.UnspentDeaths,
                                        TotalKills = playerSnapshot.TotalKills,
                                        TotalAssists = playerSnapshot.TotalAssists,
                                        TotalDeaths = playerSnapshot.TotalDeaths,
                                    };

                if (retrievalConfiguration.IncludeSummonerName)
                {
                    mutablePlayer.SummonerName = playerSnapshot.SummonerName;
                }

                if (retrievalConfiguration.IncludeTeam)
                {
                    mutablePlayer.Team = playerSnapshot.Team;
                }

                if (retrievalConfiguration.IncludeChampion)
                {
                    mutablePlayer.Champion = await ExtractChampion(playerSnapshot, clientSettings);
                }

                if (retrievalConfiguration.IncludeItems)
                {
                    mutablePlayer.Items = await ExtractItems(playerSnapshot, clientSettings);
                }

                players.Add(mutablePlayer);
            }

            return players;
        }

        private async Task<ILeagueChampion> ExtractChampion(InternalLeaguePlayerSnapshot snapshot, ILgoClientSettings clientSettings)
        {
            var retrievalConfiguration = clientSettings.LeagueChampionRetrievalConfiguration;
            var resourceChampion = await ResourceRepository.ReadChampionByNameAsync(snapshot.ChampionName) ?? throw new Exception($"Unable to retrieve {nameof(ILeagueChampion)}.");
            var mutableChampion = new MutableLeagueChampion {Id = resourceChampion.Id};

            if (retrievalConfiguration.IncludeName)
            {
                mutableChampion.Name = resourceChampion.Name;
            }

            if (retrievalConfiguration.IncludeTileImage)
            {
                mutableChampion.TileImage = await resourceChampion.Images.SquareImage.ReadContentAsBase64Async() ??
                                            throw new Exception($"Unable to retrieve {nameof(ILeagueChampion)}.{nameof(ILeagueChampion.TileImage)}.");
            }

            if (retrievalConfiguration.IncludeSplashImage)
            {
                mutableChampion.SplashImage = await resourceChampion.Images.SplashImage.ReadContentAsBase64Async() ??
                                              throw new Exception($"Unable to retrieve {nameof(ILeagueChampion)}.{nameof(ILeagueChampion.SplashImage)}.");
            }

            if (retrievalConfiguration.IncludeLoadingImage)
            {
                mutableChampion.LoadingImage = await resourceChampion.Images.LoadingImage.ReadContentAsBase64Async() ??
                                               throw new Exception($"Unable to retrieve {nameof(ILeagueChampion)}.{nameof(ILeagueChampion.LoadingImage)}.");
            }

            return mutableChampion;
        }

        private async Task<IEnumerable<ILeagueItem>> ExtractItems(InternalLeaguePlayerSnapshot snapshot, ILgoClientSettings clientSettings)
        {
            var items = new List<ILeagueItem>();
            var retrievalConfiguration = clientSettings.LeagueItemRetrievalConfiguration;

            foreach (var itemSnapshot in snapshot.Items)
            {
                var mutableItem = new MutableLeagueItem {Id = itemSnapshot.Id};
                ILeagueResourceItem? resourceItem = null;

                if (retrievalConfiguration.IncludeName)
                {
                    resourceItem ??= await ResourceRepository.ReadItemByKeyAsync(mutableItem.Id);
                    mutableItem.Name = resourceItem!.Name;
                }

                if (retrievalConfiguration.IncludeAmount)
                {
                    mutableItem.Amount = itemSnapshot.Amount;
                }

                if (retrievalConfiguration.IncludePrice)
                {
                    resourceItem ??= await ResourceRepository.ReadItemByKeyAsync(mutableItem.Id);
                    mutableItem.Price = resourceItem!.Costs.TotalCosts;
                }

                if (retrievalConfiguration.IncludeImage)
                {
                    resourceItem ??= await ResourceRepository.ReadItemByKeyAsync(mutableItem.Id);
                    mutableItem.Image = await resourceItem!.Images.SquareImage.ReadContentAsBase64Async() ?? throw new Exception($"Unable to retrieve {nameof(ILeagueItem.Image)}.");
                }

                items.Add(mutableItem);
            }

            return items;
        }

        private IEnumerable<ILeagueTeam> ExtractTeams(InternalLeagueGameSnapshot snapshot, ILgoClientSettings clientSettings)
        {
            var teams = new List<ILeagueTeam>();
            var retrievalConfiguration = clientSettings.LeagueTeamRetrievalConfiguration;

            foreach (var teamSnapshot in snapshot.Teams)
            {
                var mutableTeam = new MutableLeagueTeam
                                  {
                                      Id = teamSnapshot.Id,
                                      TotalGoldOwned = teamSnapshot.TotalGoldOwned,
                                      UnspentKills = teamSnapshot.UnspentKills,
                                      UnspentAssists = teamSnapshot.UnspentAssists,
                                      UnspentDeaths = teamSnapshot.UnspentDeaths,
                                      TotalKills = teamSnapshot.TotalKills,
                                      TotalAssists = teamSnapshot.TotalAssists,
                                      TotalDeaths = teamSnapshot.TotalDeaths,
                                  };

                if (retrievalConfiguration.IncludeSide)
                {
                    mutableTeam.Side = teamSnapshot.Side;
                }

                if (retrievalConfiguration.IncludeDragonsKilled)
                {
                    mutableTeam.DragonsKilled = teamSnapshot.DragonsKilled;
                }

                if (retrievalConfiguration.IncludeNumberOfRiftHeraldsKilled)
                {
                    mutableTeam.NumberOfRiftHeraldsKilled = teamSnapshot.NumberOfRiftHeraldsKilled;
                }

                if (retrievalConfiguration.IncludeNumberOfBaronNashorsKilled)
                {
                    mutableTeam.NumberOfBaronNashorsKilled = teamSnapshot.NumberOfBaronNashorsKilled;
                }

                if (retrievalConfiguration.IncludeTurretsDestroyed)
                {
                    mutableTeam.TurretsDestroyed = teamSnapshot.TurretsDestroyed;
                }

                if (retrievalConfiguration.IncludeInhibitorsDestroyed)
                {
                    mutableTeam.InhibitorsDestroyed = teamSnapshot.InhibitorsDestroyed;
                }

                teams.Add(mutableTeam);
            }

            return teams;
        }

        private IEnumerable<ILeagueMatchUp> ExtractMatchUps(InternalLeagueGameSnapshot gameSnapshot)
        {
            return MutableMatchUpDescriptors.Values.Select(matchUpDescriptor => BuildMatchUp(matchUpDescriptor, gameSnapshot));
        }

        private ILeagueMatchUp BuildMatchUp(ILeagueMatchUpDescriptor matchUpDescriptor, InternalLeagueGameSnapshot gameSnapshot)
        {
            if (matchUpDescriptor.Type == LeagueMatchUpDescriptorType.Player && matchUpDescriptor is ILeaguePlayerMatchUpDescriptor playerMatchUpDescriptor)
            {
                return BuildPlayerMatchUp(playerMatchUpDescriptor, gameSnapshot);
            }

            if (matchUpDescriptor.Type == LeagueMatchUpDescriptorType.Team && matchUpDescriptor is ILeagueTeamMatchUpDescriptor teamMatchUpDescriptor)
            {
                return BuildTeamMatchUp(teamMatchUpDescriptor, gameSnapshot);
            }

            throw new Exception($"Unable to build {nameof(ILeagueMatchUp)} based on the given {nameof(ILeagueMatchUpDescriptor)} (actual type = {matchUpDescriptor.GetType().FullName}).");
        }

        private ILeaguePlayerMatchUp BuildPlayerMatchUp(ILeaguePlayerMatchUpDescriptor matchUpDescriptor, InternalLeagueGameSnapshot gameSnapshot)
        {
            return PopulateMatchUp(new MutableLeaguePlayerMatchUp
                                   {
                                       Id = matchUpDescriptor.MatchUpId,
                                       Position = matchUpDescriptor.Position
                                   },
                                   ExtractGoldOwner(gameSnapshot.Players, LeagueTeamType.Blue, matchUpDescriptor.BlueSideCompetitorId),
                                   ExtractGoldOwner(gameSnapshot.Players, LeagueTeamType.Red, matchUpDescriptor.RedSideCompetitorId));
        }

        private ILeagueTeamMatchUp BuildTeamMatchUp(ILeagueTeamMatchUpDescriptor matchUpDescriptor, InternalLeagueGameSnapshot gameSnapshot)
        {
            return PopulateMatchUp(new MutableLeagueTeamMatchUp {Id = matchUpDescriptor.MatchUpId},
                                   ExtractGoldOwner(gameSnapshot.Teams, LeagueTeamType.Blue, matchUpDescriptor.BlueSideCompetitorId),
                                   ExtractGoldOwner(gameSnapshot.Teams, LeagueTeamType.Red, matchUpDescriptor.RedSideCompetitorId));
        }

        private ILeagueGoldOwner ExtractGoldOwner(IEnumerable<InternalLeaguePlayerSnapshot> players, LeagueTeamType team, Guid id)
        {
            return players.FirstOrDefault(p => p.Team == team && p.Id.Equals(id)) ??
                   throw new Exception($"Unable to extract {nameof(InternalLeaguePlayerSnapshot)} with {nameof(team)} = {team} and {nameof(id)} = {id}.");
        }

        private ILeagueGoldOwner ExtractGoldOwner(IEnumerable<InternalLeagueTeamSnapshot> teams, LeagueTeamType team, Guid id)
        {
            return teams.FirstOrDefault(t => t.Side == team && t.Id.Equals(id)) ??
                   throw new Exception($"Unable to extract {nameof(InternalLeagueTeamSnapshot)} with {nameof(team)} = {team} and {nameof(id)} = {id}.");
        }

        private TMatchUp PopulateMatchUp<TMatchUp>(TMatchUp matchUp, ILeagueGoldOwner competitorBlueSide, ILeagueGoldOwner competitorRedSide) where TMatchUp : MutableLeagueMatchUp
        {
            var goldDifference = competitorBlueSide.TotalGoldOwned - competitorRedSide.TotalGoldOwned;
            var winningTeam = goldDifference >= 0 ? LeagueTeamType.Blue : LeagueTeamType.Red;

            matchUp.BlueSideCompetitor = competitorBlueSide;
            matchUp.RedSideCompetitor = competitorRedSide;
            matchUp.GoldDifference = Math.Abs(goldDifference);
            matchUp.WinningTeam = winningTeam;

            return matchUp;
        }

        private IEnumerable<ILeagueTimer> ExtractTimers(InternalLeagueGameSnapshot snapshot, ILgoClientSettings clientSettings)
        {
            return ExtractRespawnTimers(snapshot, clientSettings).Concat(ExtractPowerPlayTimers(snapshot, clientSettings));
        }

        private IEnumerable<ILeagueTimer> ExtractRespawnTimers(InternalLeagueGameSnapshot snapshot, ILgoClientSettings clientSettings)
        {
            var result = new List<ILeagueTimer>();
            var retrievalConfiguration = clientSettings.LeagueTimerRetrievalConfiguration;
            foreach (var internalTimer in snapshot.Timers)
            {
                MutableLeagueTimer? mutableTimer = internalTimer.Type switch
                                                   {
                                                       LeagueTimerType.DragonRespawn => new MutableLeagueDragonRespawnTimer(),
                                                       LeagueTimerType.RiftHeraldRespawn => new MutableLeagueRiftHeraldRespawnTimer(),
                                                       LeagueTimerType.BaronNashorRespawn => new MutableLeagueBaronNashorRespawnTimer(),
                                                       LeagueTimerType.InhibitorRespawn => new MutableLeagueInhibitorRespawnTimer
                                                                                           {
                                                                                               Inhibitor = (internalTimer as InternalLeagueInhibitorRespawnTimerSnapshot)?.Inhibitor ??
                                                                                                           throw new
                                                                                                               Exception($"Unable to convert {internalTimer.GetType()} to {typeof(InternalLeagueInhibitorRespawnTimerSnapshot)}.")
                                                                                           },
                                                       _ => null,
                                                   };

                if (mutableTimer == null)
                {
                    continue;
                }

                mutableTimer.RemainingTimeInSeconds = internalTimer.RemainingTime.TotalSeconds;

                if (retrievalConfiguration.IncludeInGameStartTimeInSeconds)
                {
                    mutableTimer.InGameStartTimeInSeconds = internalTimer.InGameStartTime.TotalSeconds;
                }

                if (retrievalConfiguration.IncludeInGameEndTimeInSeconds)
                {
                    mutableTimer.InGameEndTimeInSeconds = internalTimer.InGameEndTime.TotalSeconds;
                }

                result.Add(mutableTimer);
            }

            return result;
        }

        private IEnumerable<ILeagueTimer> ExtractPowerPlayTimers(InternalLeagueGameSnapshot snapshot, ILgoClientSettings clientSettings)
        {
            var result = new List<ILeagueTimer>();
            var retrievalConfiguration = clientSettings.LeaguePowerPlayTimerRetrievalConfiguration;
            foreach (var internalTimer in snapshot.Timers.Where(t => t is InternalLeaguePowerPlayTimerSnapshot).Cast<InternalLeaguePowerPlayTimerSnapshot>())
            {
                MutableLeaguePowerPlayTimer mutableTimer = internalTimer.Type switch
                                                           {
                                                               LeagueTimerType.BaronNashorPowerPlay => new MutableLeagueBaronNashorPowerPlayTimer(),
                                                               LeagueTimerType.ElderDragonPowerPlay => new MutableLeagueElderDragonPowerPlayTimer(),
                                                               _ => throw new Exception($"Unknown power play type {internalTimer.Type}."),
                                                           };

                mutableTimer.RemainingTimeInSeconds = internalTimer.RemainingTime.TotalSeconds;
                mutableTimer.Team = internalTimer.Team;

                if (retrievalConfiguration.IncludeInGameStartTimeInSeconds)
                {
                    mutableTimer.InGameStartTimeInSeconds = internalTimer.InGameStartTime.TotalSeconds;
                }

                if (retrievalConfiguration.IncludeInGameEndTimeInSeconds)
                {
                    mutableTimer.InGameEndTimeInSeconds = internalTimer.InGameEndTime.TotalSeconds;
                }

                if (retrievalConfiguration.IncludeIsActive)
                {
                    mutableTimer.IsActive = internalTimer.IsActive;
                }

                if (retrievalConfiguration.IncludeMatchUps && Game.TryGetSnapshotForInGameTime(internalTimer.InGameStartTime, out var eventStartSnapshot))
                {
                    mutableTimer.MatchUps = MatchUpDescriptors.Select(m => BuildPowerPlayMatchUp(m, snapshot, internalTimer.Team, eventStartSnapshot));
                }

                result.Add(mutableTimer);
            }

            return result;
        }

        private ILeaguePowerPlayMatchUp BuildPowerPlayMatchUp(ILeagueMatchUpDescriptor matchUpDescriptor,
                                                              InternalLeagueGameSnapshot gameSnapshot,
                                                              LeagueTeamType powerPlayTeam,
                                                              InternalLeagueGameSnapshot eventStartSnapshot)
        {
            if (matchUpDescriptor.Type == LeagueMatchUpDescriptorType.Player && matchUpDescriptor is ILeaguePlayerMatchUpDescriptor playerMatchUpDescriptor)
            {
                return BuildPlayerPowerPlayMatchUp(playerMatchUpDescriptor, gameSnapshot, powerPlayTeam, eventStartSnapshot);
            }

            if (matchUpDescriptor.Type == LeagueMatchUpDescriptorType.Team && matchUpDescriptor is ILeagueTeamMatchUpDescriptor teamMatchUpDescriptor)
            {
                return BuildTeamPowerPlayMatchUp(teamMatchUpDescriptor, gameSnapshot, powerPlayTeam, eventStartSnapshot);
            }

            throw new Exception($"Unable to build {nameof(ILeagueMatchUp)} based on the given {nameof(ILeagueMatchUpDescriptor)} (actual type = {matchUpDescriptor.GetType().FullName}).");
        }

        private ILeaguePlayerPowerPlayMatchUp BuildPlayerPowerPlayMatchUp(ILeaguePlayerMatchUpDescriptor matchUpDescriptor,
                                                                          InternalLeagueGameSnapshot gameSnapshot,
                                                                          LeagueTeamType powerPlayTeam,
                                                                          InternalLeagueGameSnapshot eventStartSnapshot)
        {
            var startPlayerBlueSide = ExtractGoldOwner(eventStartSnapshot.Players, LeagueTeamType.Blue, matchUpDescriptor.BlueSideCompetitorId);
            var startPlayerRedSide = ExtractGoldOwner(eventStartSnapshot.Players, LeagueTeamType.Red, matchUpDescriptor.RedSideCompetitorId);

            var currentPlayerBlueSide = ExtractGoldOwner(gameSnapshot.Players, LeagueTeamType.Blue, matchUpDescriptor.BlueSideCompetitorId);
            var currentPlayerRedSide = ExtractGoldOwner(gameSnapshot.Players, LeagueTeamType.Red, matchUpDescriptor.RedSideCompetitorId);

            var initialGoldDifference = powerPlayTeam switch
                                        {
                                            LeagueTeamType.Blue => startPlayerBlueSide.TotalGoldOwned - startPlayerRedSide.TotalGoldOwned,
                                            LeagueTeamType.Red => startPlayerRedSide.TotalGoldOwned - startPlayerBlueSide.TotalGoldOwned,
                                            _ => throw new Exception($"Unexpected power play team: {powerPlayTeam}."),
                                        };

            var currentGoldDifference = powerPlayTeam switch
                                        {
                                            LeagueTeamType.Blue => currentPlayerBlueSide.TotalGoldOwned - currentPlayerRedSide.TotalGoldOwned,
                                            LeagueTeamType.Red => currentPlayerRedSide.TotalGoldOwned - currentPlayerBlueSide.TotalGoldOwned,
                                            _ => throw new Exception($"Unexpected power play team: {powerPlayTeam}."),
                                        };

            return PopulateMatchUp(new MutableLeaguePlayerPowerPlayMatchUp
                                   {
                                       Id = matchUpDescriptor.MatchUpId,
                                       Position = matchUpDescriptor.Position,
                                       GoldDifferenceIncrease = currentGoldDifference - initialGoldDifference,
                                   }, currentPlayerBlueSide, currentPlayerRedSide);
        }

        private ILeagueTeamPowerPlayMatchUp BuildTeamPowerPlayMatchUp(ILeagueTeamMatchUpDescriptor matchUpDescriptor,
                                                                      InternalLeagueGameSnapshot gameSnapshot,
                                                                      LeagueTeamType powerPlayTeam,
                                                                      InternalLeagueGameSnapshot eventStartSnapshot)
        {
            var startPlayerBlueSide = ExtractGoldOwner(eventStartSnapshot.Players, LeagueTeamType.Blue, matchUpDescriptor.BlueSideCompetitorId);
            var startPlayerRedSide = ExtractGoldOwner(eventStartSnapshot.Players, LeagueTeamType.Red, matchUpDescriptor.RedSideCompetitorId);

            var currentPlayerBlueSide = ExtractGoldOwner(gameSnapshot.Players, LeagueTeamType.Blue, matchUpDescriptor.BlueSideCompetitorId);
            var currentPlayerRedSide = ExtractGoldOwner(gameSnapshot.Players, LeagueTeamType.Red, matchUpDescriptor.RedSideCompetitorId);

            var initialGoldDifference = powerPlayTeam switch
                                        {
                                            LeagueTeamType.Blue => startPlayerBlueSide.TotalGoldOwned - startPlayerRedSide.TotalGoldOwned,
                                            LeagueTeamType.Red => startPlayerRedSide.TotalGoldOwned - startPlayerBlueSide.TotalGoldOwned,
                                            _ => throw new Exception($"Unexpected power play team: {powerPlayTeam}."),
                                        };

            var currentGoldDifference = powerPlayTeam switch
                                        {
                                            LeagueTeamType.Blue => currentPlayerBlueSide.TotalGoldOwned - currentPlayerRedSide.TotalGoldOwned,
                                            LeagueTeamType.Red => currentPlayerRedSide.TotalGoldOwned - currentPlayerBlueSide.TotalGoldOwned,
                                            _ => throw new Exception($"Unexpected power play team: {powerPlayTeam}."),
                                        };

            return PopulateMatchUp(new MutableLeagueTeamPowerPlayMatchUp
                                   {
                                       Id = matchUpDescriptor.MatchUpId,
                                       GoldDifferenceIncrease = currentGoldDifference - initialGoldDifference,
                                   }, currentPlayerBlueSide, currentPlayerRedSide);
        }
    }
}