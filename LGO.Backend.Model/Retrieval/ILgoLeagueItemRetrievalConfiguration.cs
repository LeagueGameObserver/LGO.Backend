using LGO.Backend.Model.Retrieval.Enum;

namespace LGO.Backend.Model.Retrieval
{
    public interface ILgoLeagueItemRetrievalConfiguration : ILgoDataRetrievalConfiguration
    {
        LgoDataRetrievalConfigurationType ILgoDataRetrievalConfiguration.Type => LgoDataRetrievalConfigurationType.LeagueItem;
        
        bool IncludeName { get; }
        
        bool IncludeAmount { get; }
        
        bool IncludePrice { get; }
        
        bool IncludeImage { get; }
    }
}