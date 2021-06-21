using System;
using System.Threading.Tasks;
using LGO.Backend.Tool.Replay.LocalClientGameReplayService;
using log4net;

namespace LGO.Backend.Tool.Replay.GameRecorder
{
    public static class Program
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(Program));
        
        private static LocalClientGameReplayRecorder? _recorder;

        public static async Task Main(string[] args)
        {
            await StartNewRecorder();

            while (true)
            {
                await Task.Delay(1);
            }

            _recorder?.Stop();
        }

        private static async Task StartNewRecorder()
        {
            Log.Info($"Starting new {nameof(LocalClientGameReplayRecorder)}.");
            
            _recorder = await LocalClientGameReplayRecorder.ForLocalGame();
            if (_recorder == null)
            {
                throw new Exception($"Unable to instantiate new {nameof(LocalClientGameReplayRecorder)}.");
            }

            _recorder.HasStopped += Recorder_OnHasStopped;
            _recorder.Start();
        }

        private static void Recorder_OnHasStopped(object? sender, EventArgs e)
        {
            Log.Info($"{nameof(LocalClientGameReplayRecorder)} has stopped.");
            StartNewRecorder();
        }
    }
}