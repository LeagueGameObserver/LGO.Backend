using LGO.Backend.Model.Retrieval.Enum;

namespace LGO.Backend.Model.Retrieval
{
    public interface ILgoLeaguePowerPlayTimerRetrievalConfiguration : ILgoLeagueTimerRetrievalConfiguration
    {
        LgoDataRetrievalConfigurationType ILgoDataRetrievalConfiguration.Type => LgoDataRetrievalConfigurationType.LeaguePowerPlayTimer;

        bool IncludeIsActive { get; }
        
        bool IncludeMatchUps { get; }
    }
}