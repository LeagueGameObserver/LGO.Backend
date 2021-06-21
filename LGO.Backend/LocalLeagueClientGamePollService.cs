using System;
using System.Threading;
using System.Threading.Tasks;
using LGO.Backend.Core.Http;
using LGO.Backend.Model;
using LGO.LeagueClient.LocalGameReader;
using LGO.LeagueClient.Model.Game;
using log4net;

namespace LGO.Backend
{
    public sealed class LocalLeagueClientGamePollService : ILeagueClientGameService
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LocalLeagueClientGamePollService));

        public event EventHandler<ILeagueClientGame>? GameDataReceived;

        public Guid Id { get; } = Guid.NewGuid();
        
        public ILeagueClientGameRetrievalMetadata RetrievalMetadata { get; }
        
        private static TimeSpan DefaultPollDelay { get; } = TimeSpan.FromMilliseconds(500);
        private static TimeSpan DefaultRequestTimeout { get; } = TimeSpan.FromSeconds(1);
        
        public bool IsRunning { get; private set; } = false;

        private object Lock { get; } = new();
        private LocalLeagueClientGameReader GameReader { get; }
        private Task? _pollingTask;
        private CancellationTokenSource _pollingTaskCancellationTokenSource = new();

        public LocalLeagueClientGamePollService(ILeagueClientGameRetrievalMetadata retrievalMetadata, TimeSpan? httpRequestTimeout = null)
        {
            RetrievalMetadata = retrievalMetadata;
            GameReader = new LocalLeagueClientGameReader(new JsonHttpClient(httpRequestTimeout ?? DefaultRequestTimeout));
        }

        public void Start()
        {
            lock (Lock)
            {
                if (IsRunning)
                {
                    return;
                }
                
                Log.Info($"Starting {nameof(LocalLeagueClientGamePollService)}.");

                IsRunning = true;
                _pollingTaskCancellationTokenSource = new CancellationTokenSource();
                _pollingTask = Task.Run(PollWhileRunning);
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
                
                Log.Info($"Stopping {nameof(LocalLeagueClientGamePollService)}.");

                IsRunning = false;
                _pollingTaskCancellationTokenSource.Cancel();
                _pollingTask?.Wait();
            }
        }

        private async Task PollWhileRunning()
        {
            while (IsRunning)
            {
                try
                {
                    var gameData = await GameReader.ReadGameAsync();
                    if (gameData != null)
                    {
                        GameDataReceived?.Invoke(this, gameData);
                    }
                }
                catch (Exception exception)
                {
                    Log.Error($"Exception in {nameof(PollWhileRunning)}.", exception);
                }

                try
                {
                    if (_pollingTaskCancellationTokenSource != null)
                    {
                        await Task.Delay(RetrievalMetadata.PollingInterval ?? DefaultPollDelay, _pollingTaskCancellationTokenSource.Token);
                    }
                    else
                    {
                        await Task.Delay(RetrievalMetadata.PollingInterval ?? DefaultPollDelay);
                    }
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }
    }
}