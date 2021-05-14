using System;
using System.Threading;
using System.Threading.Tasks;
using LGO.Backend.Core.Http;
using LGO.LeagueClient.LocalGameReader;
using LGO.LeagueClient.Model.Game;
using log4net;

namespace LGO.Backend
{
    public sealed class LocalLeagueClientGamePollService : ILeagueClientGameService
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LocalLeagueClientGamePollService));

        public Guid Id { get; } = Guid.NewGuid();
        
        private static TimeSpan DefaultPollDelay { get; } = TimeSpan.FromMilliseconds(500);
        private static TimeSpan DefaultRequestTimeout { get; } = TimeSpan.FromSeconds(1);

        public event EventHandler<ILeagueClientGame>? GameDataReceived;

        public bool IsRunning { get; private set; } = false;

        private object Lock { get; } = new();
        private TimeSpan PollDelay { get; }
        private LocalLeagueClientGameReader GameReader { get; }
        private Task? _pollingTask;
        private CancellationTokenSource? _pollingTaskCancellationTokenSource = new();

        public LocalLeagueClientGamePollService() : this(DefaultPollDelay, DefaultRequestTimeout)
        {
        }

        public LocalLeagueClientGamePollService(TimeSpan pollDelay, TimeSpan requestTimeout)
        {
            PollDelay = pollDelay;
            GameReader = new LocalLeagueClientGameReader(new JsonHttpClient(requestTimeout));
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
                _pollingTaskCancellationTokenSource?.Cancel();
                _pollingTask?.Wait();
                _pollingTask = null;
                _pollingTaskCancellationTokenSource = null;
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
                        await Task.Delay(PollDelay, _pollingTaskCancellationTokenSource.Token);
                    }
                    else
                    {
                        await Task.Delay(PollDelay);
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