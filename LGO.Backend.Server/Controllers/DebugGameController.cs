// using System;
// using System.Collections.Concurrent;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Net;
// using System.Threading.Tasks;
// using LGO.Backend.Core.Model.League.Enum;
// using LGO.Backend.Core.Utility;
// using LGO.Backend.Model;
// using LGO.Backend.Model.League;
// using LGO.LeagueClient.LocalGameReader;
// using LGO.LeagueClient.Model;
// using LGO.LeagueClient.Model.Game;
// using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
//
// namespace LGO.Backend.Server.Controllers
// {
//     [Route("debug/api/Game")]
//     [ApiController]
//     public class DebugGameController : ControllerBase
//     {
//         public sealed class DebugGameConfiguration
//         {
//             public string PathToGameSnapshots { get; set; } = string.Empty;
//         }
//
//         private sealed class DebugGameReader : ILeagueClientGameReader
//         {
//             private int _nextGameSnapshotIndex = 0;
//
//             public int NextGameSnapshotIndex
//             {
//                 get => _nextGameSnapshotIndex;
//                 set => _nextGameSnapshotIndex = Math.Max(0, Math.Min(GameSnapshots.Count - 1, value));
//             }
//
//             public IReadOnlyList<FileInfo> GameSnapshots { get; }
//
//             public DebugGameReader(DirectoryInfo pathToGameSnapshots)
//             {
//                 GameSnapshots = pathToGameSnapshots.GetFiles().Where(f => f.Extension.Equals(".json", StringComparison.InvariantCultureIgnoreCase)).OrderBy(f => f.Name).ToList();
//                 if (GameSnapshots.Count < 1)
//                 {
//                     throw new ArgumentException($"No game snapshots in \"{pathToGameSnapshots.FullName}\" found.");
//                 }
//             }
//
//             public Task<ILeagueClientGame?> ReadGameAsync()
//             {
//                 return LocalLeagueClientGameReader.ReadGameSnapshotFromFile(GameSnapshots[NextGameSnapshotIndex]);
//             }
//         }
//
//         private sealed class DebugGameReaderService : ILeagueClientGameService
//         {
//             public Guid Id { get; } = Guid.NewGuid();
//
//             public DebugGameReader GameReader { get; }
//
//             public event EventHandler<ILeagueClientGame>? GameDataReceived;
//
//             public bool IsRunning => true;
//
//             public DebugGameReaderService(DebugGameReader gameReader)
//             {
//                 GameReader = gameReader;
//             }
//
//             public void Start()
//             {
//             }
//
//             public void Stop()
//             {
//             }
//
//             public async Task EmitUpdate()
//             {
//                 var gameSnapshot = await GameReader.ReadGameAsync();
//                 if (gameSnapshot == null)
//                 {
//                     return;
//                 }
//
//                 GameDataReceived?.Invoke(this, gameSnapshot);
//             }
//         }
//
//         private ConcurrentDictionary<Guid, DebugGameReaderService> GameReaderServices { get; } = new();
//         private ConcurrentDictionary<Guid, Guid> GameIdToServiceIdMapping { get; } = new();
//
//         [HttpPost]
//         public async Task<ILeagueGameSummary> CreateNewDebugGame(DebugGameConfiguration gameConfiguration)
//         {
//             var pathToSnapshots = new DirectoryInfo(gameConfiguration.PathToGameSnapshots);
//             if (!pathToSnapshots.Exists)
//             {
//                 throw new ArgumentException($"Path to debug game snapshots (\"{gameConfiguration.PathToGameSnapshots}\") does not exist.");
//             }
//
//             var reader = new DebugGameReader(pathToSnapshots);
//             var firstGameSnapshot = await reader.ReadGameAsync();
//             if (firstGameSnapshot == null)
//             {
//                 throw new Exception($"Unable to retrieve first game snapshot from newly created {nameof(DebugGameReader)}.");
//             }
//
//             var service = new DebugGameReaderService(reader);
//             if (!GameReaderServices.TryAdd(service.Id, service))
//             {
//                 throw new Exception($"Unable to add newly created {nameof(DebugGameReader)} to {nameof(DebugGameReaderService)}.");
//             }
//
//             if (!Program.Backend.AddGameService(service))
//             {
//                 throw new Exception($"Unable to add newly created {nameof(DebugGameReaderService)} to {nameof(LgoBackend)}.");
//             }
//
//             await service.EmitUpdate();
//             var comparer = new MultiSetEqualityComparer<string>();
//             var playersBlueSide = firstGameSnapshot.Players.Where(p => p.Team == LeagueTeamType.Blue).Select(p => p.SummonerName).ToList();
//             var playersRedSide = firstGameSnapshot.Players.Where(p => p.Team == LeagueTeamType.Red).Select(p => p.SummonerName).ToList();
//
//             foreach (var summary in Program.Backend.GameSummaries)
//             {
//                 if (Math.Abs(summary.InGameTimeInSeconds - firstGameSnapshot.Stats.InGameTime.TotalSeconds) > 0.1d)
//                 {
//                     continue;
//                 }
//
//                 if (summary.Mode != firstGameSnapshot.Stats.GameMode)
//                 {
//                     continue;
//                 }
//
//                 if (!comparer.Equals(playersBlueSide, summary.SummonerNamesBlueTeam))
//                 {
//                     continue;
//                 }
//
//                 if (!comparer.Equals(playersRedSide, summary.SummonerNamesRedSide))
//                 {
//                     continue;
//                 }
//
//                 if (!GameIdToServiceIdMapping.TryAdd(summary.Id, service.Id))
//                 {
//                     throw new Exception($"Unable to create game id to game reader service id mapping for newly created debug game from path \"{gameConfiguration.PathToGameSnapshots}\".");
//                 }
//                 
//                 return summary;
//             }
//
//             throw new Exception($"Unable to find {nameof(ILeagueGameSummary)} for newly created debug game from path \"{gameConfiguration.PathToGameSnapshots}\".");
//         }
//     }
// }