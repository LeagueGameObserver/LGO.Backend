using LGO.Backend.Model.Retrieval;

namespace LGO.Backend.Model
{
    public interface ILgoClientSettings
    {
        ILgoLeagueChampionRetrievalConfiguration LeagueChampionRetrievalConfiguration { get; }
        
        ILgoLeagueGameRetrievalConfiguration LeagueGameRetrievalConfiguration { get; }
        
        ILgoLeagueItemRetrievalConfiguration LeagueItemRetrievalConfiguration { get; }
        
        ILgoLeaguePlayerRetrievalConfiguration LeaguePlayerRetrievalConfiguration { get; }
        
        ILgoLeaguePowerPlayTimerRetrievalConfiguration LeaguePowerPlayTimerRetrievalConfiguration { get; }
        
        ILgoLeagueTeamRetrievalConfiguration LeagueTeamRetrievalConfiguration { get; }
        
        ILgoLeagueTimerRetrievalConfiguration LeagueTimerRetrievalConfiguration { get; }
    }
}