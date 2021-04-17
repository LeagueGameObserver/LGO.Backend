using LGO.Backend.Model.Retrieval.Enum;

namespace LGO.Backend.Model.Retrieval
{
    public interface ILgoLeagueTeamRetrievalConfiguration : ILgoDataRetrievalConfiguration
    {
        LgoDataRetrievalConfigurationType ILgoDataRetrievalConfiguration.Type => LgoDataRetrievalConfigurationType.LeagueTeam;

        bool IncludeSide { get; }

        bool IncludeDragonsKilled { get; }

        bool IncludeNumberOfRiftHeraldsKilled { get; }

        bool IncludeNumberOfBaronNashorsKilled { get; }

        bool IncludeTurretsDestroyed { get; }

        bool IncludeInhibitorsDestroyed { get; }
    }
}