using System;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Model
{
    internal sealed class MutableLeagueClientGameRetrievalMetadata : ILeagueClientGameRetrievalMetadata
    {
        public TimeSpan? PollingInterval { get; set; } = null;
        public MultiComponentVersion GameVersion { get; set; } = null!;
        public LeagueLocalizationType GameLocalization { get; set; } = LeagueLocalizationType.EnglishUnitedStates;
    }
}