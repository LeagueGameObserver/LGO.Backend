using LGO.Backend.Model.Retrieval.Enum;

namespace LGO.Backend.Model.Retrieval
{
    public interface ILgoDataRetrievalConfiguration
    {
        LgoDataRetrievalConfigurationType Type { get; }
    }
}