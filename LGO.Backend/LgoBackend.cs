using System;
using System.Collections.Concurrent;
using System.Linq;
using LGO.Backend.League;
using LGO.Backend.Model;
using LGO.Backend.Model.League.Enum;
using LGO.LeagueClient.Model.Game;
using LGO.LeagueClient.Model.GameEvent;
using LGO.LeagueResource.Model;
using log4net;

namespace LGO.Backend
{
    public sealed class LgoBackend
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LgoBackend));

        public event EventHandler<Guid>? GameCreated;

        private ILeagueResourceRepository ResourceRepository { get; }
        private ILeagueGameConstantsFactory GameConstantsFactory { get; }
        private ConcurrentDictionary<Guid, ILeagueClientGameService> GameServices { get; } = new();
        private ConcurrentDictionary<int, InternalLeagueGame> GamesBySummonerNamesHash { get; } = new();
        private ConcurrentDictionary<Guid, int> GameIdToSummonerNamesHashIndex { get; } = new();

        public LgoBackend(ILeagueResourceRepository resourceRepository, ILeagueGameConstantsFactory gameConstantsFactory)
        {
            ResourceRepository = resourceRepository;
            GameConstantsFactory = gameConstantsFactory;
        }

        public bool TryCreateGameReader(Guid gameId, out ILeagueGameReader gameReader)
        {
            gameReader = null!;
            if (!GameIdToSummonerNamesHashIndex.TryGetValue(gameId, out var gameHash))
            {
                return false;
            }

            if (!GamesBySummonerNamesHash.TryGetValue(gameHash, out var game))
            {
                return false;
            }

            gameReader = new InternalLeagueGameReader(game, ResourceRepository);
            return true;
        }

        public bool AddGameService(ILeagueClientGameService service)
        {
            Log.Debug($"Trying to add new {nameof(ILeagueClientGameService)} (id = {service.Id}, actual type = {service.GetType().FullName}).");

            if (!GameServices.TryAdd(service.Id, service))
            {
                Log.Warn($"Failed to add new {nameof(ILeagueClientGameService)} (id = {service.Id}, actual type = {service.GetType().FullName}).");
                return false;
            }

            Log.Debug($"Added new {nameof(ILeagueClientGameService)} (id = {service.Id}, actual type = {service.GetType().FullName}).");
            service.GameDataReceived += GameService_OnGameDataReceived;
            service.Start();
            return true;
        }

        public bool RemoveGameService(ILeagueClientGameService service)
        {
            Log.Debug($"Trying to remove {nameof(ILeagueClientGameService)} (id = {service.Id}, actual type = {service.GetType().FullName}).");

            if (!GameServices.TryRemove(service.Id, out _))
            {
                Log.Warn($"Failed to remove {nameof(ILeagueClientGameService)} (id = {service.Id}, actual type = {service.GetType().FullName}).");
                return false;
            }

            Log.Debug($"Removed {nameof(ILeagueClientGameService)} (id = {service.Id}, actual type = {service.GetType().FullName}).");
            service.GameDataReceived -= GameService_OnGameDataReceived;
            return true;
        }

        private void GameService_OnGameDataReceived(object? sender, ILeagueClientGame e)
        {
            if (sender is not ILeagueClientGameService gameService)
            {
                return;
            }

            Log.Debug($"Received new {nameof(ILeagueClientGame)} (actual type = {e.GetType().FullName}) from {nameof(ILeagueClientGameService)} (id = {gameService.Id}).");

            var gameHashCode = SummonerNamesHash(e);

            var newGameCreated = false;
            var game = GamesBySummonerNamesHash.GetOrAdd(gameHashCode, _ =>
                                                                       {
                                                                           newGameCreated = true;
                                                                           var gameConstants = GameConstantsFactory.ForMapAndMode(e.Stats.Map, e.Stats.GameMode);
                                                                           var newGame = new InternalLeagueGame(ResourceRepository, gameConstants, e);
                                                                           GameIdToSummonerNamesHashIndex.TryAdd(newGame.Id, gameHashCode);

                                                                           Log.Info($"Created new {nameof(InternalLeagueGame)} (id = {newGame.Id})");

                                                                           return newGame;
                                                                       });

            if (newGameCreated)
            {
                GameCreated?.Invoke(this, game.Id);
            }
            else
            {
                game.Update(e);
            }

            if (e.EventCollection.Events.Any(@event => @event.Type == LeagueClientGameEventType.GameEnded))
            {
                Log.Info($"Received a new {nameof(ILeagueClientGame)} (actual type = {e.GetType().FullName}) with {nameof(LeagueGameEventType.GameEnded)} event.");
                // game has ended
                RemoveGameService(gameService);
                GamesBySummonerNamesHash.TryRemove(gameHashCode, out _);
                GameIdToSummonerNamesHashIndex.TryRemove(game.Id, out _);
            }
        }

        private static int SummonerNamesHash(ILeagueClientGame game)
        {
            var sortedSummonerNames = game.Players.OrderBy(p => p.SummonerName).Select(p => p.SummonerName).ToList();
            return sortedSummonerNames.GetHashCode();
        }
    }
}