using System;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model
{
    public interface ILeagueClientGameRetrievalMetadata
    {
        TimeSpan? PollingInterval { get; }
        
        MultiComponentVersion GameVersion { get; }
        
        LeagueLocalizationType GameLocalization { get; }
    }
}