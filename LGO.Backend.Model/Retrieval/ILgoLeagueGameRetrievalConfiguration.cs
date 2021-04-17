using LGO.Backend.Model.Retrieval.Enum;

namespace LGO.Backend.Model.Retrieval
{
    public interface ILgoLeagueGameRetrievalConfiguration : ILgoDataRetrievalConfiguration
    {
        LgoDataRetrievalConfigurationType ILgoDataRetrievalConfiguration.Type => LgoDataRetrievalConfigurationType.LeagueGame;
        
        bool IncludeInGameTimeInSeconds { get; }
        
        bool IncludeMode { get; }
        
        bool IncludeTeams { get; }
        
        bool IncludePlayers { get; }
        
        bool IncludeMatchUps { get; }
        
        bool IncludeTimers { get; }
        
        bool IncludeEvents { get; }
        
        bool IncludeEventsSinceLastUpdate { get; }
    }
}