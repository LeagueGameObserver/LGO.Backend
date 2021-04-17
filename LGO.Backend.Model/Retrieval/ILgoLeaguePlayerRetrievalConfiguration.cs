using LGO.Backend.Model.Retrieval.Enum;

namespace LGO.Backend.Model.Retrieval
{
    public interface ILgoLeaguePlayerRetrievalConfiguration : ILgoDataRetrievalConfiguration
    {
        LgoDataRetrievalConfigurationType ILgoDataRetrievalConfiguration.Type => LgoDataRetrievalConfigurationType.LeaguePlayer;

        bool IncludeSummonerName { get; }

        bool IncludeTeam { get; }

        bool IncludeChampion { get; }

        bool IncludeItems { get; }
    }
}