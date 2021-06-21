using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueApi.RemoteApiReader.Static;
using LGO.LeagueClient.LocalGameReader;
using log4net;

namespace LGO.Backend.Tool.Replay.LocalClientGameReplayService
{
    public sealed class LocalClientGameReplayRecorder
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LocalClientGameReplayRecorder));

        private static TimeSpan DefaultPollingInterval { get; } = TimeSpan.FromSeconds(1);
        private const LeagueLocalizationType DefaultGameLocalization = LeagueLocalizationType.EnglishUnitedStates;

        public event EventHandler? HasStopped;

        public static async Task<LocalClientGameReplayRecorder?> ForLocalGame()
        {
            var staticApiReader = new RemoteLeagueStaticApiReader();
            var availableGameVersions = await staticApiReader.ReadGameVersionsAsync();
            if (availableGameVersions == null)
            {
                return null;
            }

            var currentGameVersion = availableGameVersions.FirstOrDefault();
            if (currentGameVersion == null)
            {
                return null;
            }

            var snapshotDirectory = GetNextSnapshotDirectory();
            var metadata = new LocalClientGameReplayRetrievalMetadata(DefaultPollingInterval, currentGameVersion, DefaultGameLocalization, snapshotDirectory);

            return new LocalClientGameReplayRecorder(metadata);
        }

        public static DirectoryInfo GetNextSnapshotDirectory()
        {
            const string outputDirectoryPrefix = "LGO-RecordedLocalClientGameSnapshots";

            var outputDirectoryCounter = 0;
            do
            {
                if (Directory.Exists($"./{outputDirectoryPrefix}-{outputDirectoryCounter:0000}"))
                {
                    outputDirectoryCounter++;
                    continue;
                }

                break;
            } while (true);

            var outputDirectory = new DirectoryInfo($"./{outputDirectoryPrefix}-{outputDirectoryCounter:0000}");
            return outputDirectory;
        }

        public LocalClientGameReplayRetrievalMetadata RetrievalMetadata { get; }
        public bool IsRunning { get; private set; }

        private object Lock { get; } = new();
        private bool _hasBeenStarted;
        private Task? _pollingTask;
        private CancellationTokenSource _pollingTaskCancellationTokenSource = new();

        public LocalClientGameReplayRecorder(LocalClientGameReplayRetrievalMetadata retrievalMetadata)
        {
            RetrievalMetadata = retrievalMetadata;

            Log.Info($"Created new {nameof(LocalClientGameReplayRecorder)} with following parameters: " +
                     $"{nameof(RetrievalMetadata.PollingInterval)} = {RetrievalMetadata.PollingInterval}; " +
                     $"{nameof(RetrievalMetadata.GameVersion)} = {RetrievalMetadata.GameVersion}; " +
                     $"{nameof(RetrievalMetadata.GameLocalization)} = {RetrievalMetadata.GameLocalization}; " +
                     $"{nameof(RetrievalMetadata.SnapshotDirectory)} = \"{RetrievalMetadata.SnapshotDirectory.FullName}\"");
        }

        public void Start()
        {
            lock (Lock)
            {
                if (IsRunning || _hasBeenStarted)
                {
                    return;
                }

                IsRunning = true;
                _hasBeenStarted = true;
                _pollingTaskCancellationTokenSource = new CancellationTokenSource();
                _pollingTask = Task.Run(PollWhileGameIsRunning);
            }
        }

        private async Task PollWhileGameIsRunning()
        {
            var snapshotCounter = 0;
            var snapshot = await GetFirstSnapshot();
            if (string.IsNullOrEmpty(snapshot))
            {
                return;
            }

            if (Directory.Exists(RetrievalMetadata.SnapshotDirectory.FullName))
            {
                Directory.Delete(RetrievalMetadata.SnapshotDirectory.FullName, true);
            }

            Directory.CreateDirectory(RetrievalMetadata.SnapshotDirectory.FullName);

            await RetrievalMetadata.WriteToFile(new FileInfo(Path.Combine(RetrievalMetadata.SnapshotDirectory.FullName, LocalClientGameReplayRetrievalMetadata.MetadataFileName)));

            do
            {
                await File.WriteAllTextAsync(Path.Combine(RetrievalMetadata.SnapshotDirectory.FullName, $"{LocalClientGameReplayService.SnapshotFileNamePrefix}-{snapshotCounter:0000}.json"),
                                             snapshot);
                snapshotCounter++;
                try
                {
                    await Task.Delay(RetrievalMetadata.PollingInterval ?? DefaultPollingInterval, _pollingTaskCancellationTokenSource.Token);
                }
                catch (Exception)
                {
                    break;
                }
                
                snapshot = await PollNextSnapshot();
            } while (IsRunning && !string.IsNullOrEmpty(snapshot));

            Log.Info($"Done polling local game snapshots. Received {snapshotCounter} snapshots in total.");
            Stop();
        }

        private async Task<string?> GetFirstSnapshot()
        {
            Log.Info("Started polling local game data.");
            while (IsRunning)
            {
                try
                {
                    var snapshot = await PollNextSnapshot();
                    if (!string.IsNullOrEmpty(snapshot))
                    {
                        Log.Info("Received first local game snapshot.");
                        return snapshot;
                    }

                    await Task.Delay(RetrievalMetadata.PollingInterval ?? DefaultPollingInterval, _pollingTaskCancellationTokenSource.Token);
                }
                catch (Exception)
                {
                    break;
                }
            }

            return null;
        }

        private static async Task<string?> PollNextSnapshot()
        {
            return await LocalLeagueClientGameReader.DefaultClient.GetRawAsync(LocalLeagueClientGameReader.GameDataUrl);
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

                HasStopped?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}