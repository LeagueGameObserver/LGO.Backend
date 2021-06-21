using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LGO.Backend.Model;
using LGO.LeagueClient.LocalGameReader;
using LGO.LeagueClient.Model.Game;
using log4net;

namespace LGO.Backend.Tool.Replay.LocalClientGameReplayService
{
    public sealed class LocalClientGameReplayService : ILeagueClientGameService
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LocalClientGameReplayService));

        public const string SnapshotFileNamePrefix = "snapshot";

        private static TimeSpan DefaultPollingInterval { get; } = TimeSpan.FromSeconds(1);

        public event EventHandler<ILeagueClientGame>? GameDataReceived;
        public Guid Id { get; } = Guid.NewGuid();
        public ILeagueClientGameRetrievalMetadata RetrievalMetadata { get; }

        public LocalClientGameReplayServiceSettings ReplaySettings { get; set; } = new();

        public bool IsRunning { get; private set; }

        private int _nextGameSnapshotIndex;

        public int NextGameSnapshotIndex
        {
            get => Math.Max(0, Math.Min(SortedGameSnapshots.Count - 1, _nextGameSnapshotIndex));
            set => _nextGameSnapshotIndex = Math.Max(0, Math.Min(SortedGameSnapshots.Count, value));
        }

        public bool IsAtEnd => _nextGameSnapshotIndex >= SortedGameSnapshots.Count;

        private object Lock { get; } = new();
        private IReadOnlyList<FileInfo> SortedGameSnapshots { get; }
        private CancellationTokenSource _pollingTaskCancellationTokenSource = new();
        private Task? _pollingTask;

        public LocalClientGameReplayService(LocalClientGameReplayRetrievalMetadata retrievalMetadata)
        {
            RetrievalMetadata = retrievalMetadata;
            SortedGameSnapshots = retrievalMetadata.SnapshotDirectory
                                                   .GetFiles()
                                                   .Where(f => f.Extension.Equals(".json", StringComparison.InvariantCultureIgnoreCase))
                                                   .Where(f => f.Name.StartsWith(SnapshotFileNamePrefix))
                                                   .OrderBy(f => f.Name)
                                                   .ToList();
        }

        public void Start()
        {
            lock (Lock)
            {
                if (IsRunning)
                {
                    return;
                }

                IsRunning = true;
                _pollingTaskCancellationTokenSource = new CancellationTokenSource();
                _pollingTask = Task.Run(PollWhileRunning);
            }
        }

        private async Task PollWhileRunning()
        {
            while (IsRunning)
            {
                try
                {
                    var gameData = await LocalLeagueClientGameReader.ReadRawGameSnapshotFromFile(SortedGameSnapshots[NextGameSnapshotIndex]);
                    if (gameData != null)
                    {
                        GameDataReceived?.Invoke(this, gameData);
                    }
                }
                catch (Exception exception)
                {
                    Log.Error($"Exception in {nameof(PollWhileRunning)}.", exception);
                }

                do
                {
                    try
                    {
                        await Task.Delay(RetrievalMetadata.PollingInterval ?? DefaultPollingInterval, _pollingTaskCancellationTokenSource.Token);
                    }
                    catch (Exception)
                    {
                        // ignore
                    }
                } while (IsRunning && ReplaySettings.ReplaySpeed == 0);


                if (!IsAtEnd)
                {
                    NextGameSnapshotIndex += ReplaySettings.ReplaySpeed;
                    continue;
                }

                if (ReplaySettings.Loop)
                {
                    NextGameSnapshotIndex = 0;
                    continue;
                }

                Stop();
            }
        }

        public void Stop()
        {
            lock (Lock)
            {
                if (!IsRunning)
                {
                    return;
                }

                IsRunning = false;
                _pollingTaskCancellationTokenSource.Cancel();
                _pollingTask?.Wait();
            }
        }
    }
}