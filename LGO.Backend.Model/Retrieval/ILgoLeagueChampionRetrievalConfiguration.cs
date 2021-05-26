using LGO.Backend.Model.Retrieval.Enum;

namespace LGO.Backend.Model.Retrieval
{
    public interface ILgoLeagueChampionRetrievalConfiguration : ILgoDataRetrievalConfiguration
    {
        LgoDataRetrievalConfigurationType ILgoDataRetrievalConfiguration.Type => LgoDataRetrievalConfigurationType.LeagueChampion;

        bool IncludeName { get; }
        
        bool IncludeTileImage { get; }
        
        bool IncludeSplashImage { get; }
        
        bool IncludeLoadingImage { get; }
    }
}