using LGO.Backend.Model.Retrieval.Enum;

namespace LGO.Backend.Model.Retrieval
{
    public interface ILgoLeagueTimerRetrievalConfiguration : ILgoDataRetrievalConfiguration
    {
        LgoDataRetrievalConfigurationType ILgoDataRetrievalConfiguration.Type => LgoDataRetrievalConfigurationType.LeagueTimer;

        bool IncludeInGameStartTimeInSeconds { get; }

        bool IncludeInGameEndTimeInSeconds { get; }
    }
}