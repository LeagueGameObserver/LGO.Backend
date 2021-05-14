using System;
using LGO.LeagueClient.Model.Game;

namespace LGO.Backend
{
    public interface ILeagueClientGameService
    {
        Guid Id { get; }
        
        event EventHandler<ILeagueClientGame> GameDataReceived;
        
        bool IsRunning { get; }

        void Start();

        void Stop();
    }
}