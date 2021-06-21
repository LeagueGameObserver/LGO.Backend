using System;
using LGO.LeagueClient.Model.Game;

namespace LGO.Backend.Model
{
    public interface ILeagueClientGameService
    {
        event EventHandler<ILeagueClientGame> GameDataReceived;
        
        Guid Id { get; }
        
        ILeagueClientGameRetrievalMetadata RetrievalMetadata { get; }

        bool IsRunning { get; }

        void Start();

        void Stop();
    }
}