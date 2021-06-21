using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Model;
using LGO.Backend.Model.Retrieval;
using Newtonsoft.Json;

namespace LGO.Backend
{
    public sealed class ImmutableClientSettings : ILgoClientSettings
    {
        public static ImmutableClientSettings Default { get; } = new()
                                                                 {
                                                                     LeagueChampionRetrievalConfiguration = MutableLeagueChampionRetrievalConfiguration.IncludeEverything,
                                                                     LeagueGameRetrievalConfiguration = MutableLeagueGameRetrievalConfiguration.IncludeEverything,
                                                                     LeagueItemRetrievalConfiguration = MutableLeagueItemRetrievalConfiguration.IncludeEverything,
                                                                     LeaguePlayerRetrievalConfiguration = MutableLeaguePlayerRetrievalConfiguration.IncludeEverything,
                                                                     LeaguePowerPlayTimerRetrievalConfiguration = MutableLeaguePowerPlayRetrievalConfiguration.IncludeEverything,
                                                                     LeagueTeamRetrievalConfiguration = MutableLeagueTeamRetrievalConfiguration.IncludeEverything,
                                                                     LeagueTimerRetrievalConfiguration = MutableLeagueTimerRetrievalConfiguration.IncludeEverything,
                                                                 };

        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueChampionRetrievalConfiguration, MutableLeagueChampionRetrievalConfiguration>))]
        public ILgoLeagueChampionRetrievalConfiguration LeagueChampionRetrievalConfiguration { get; init; } = MutableLeagueChampionRetrievalConfiguration.IncludeEverything;

        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueGameRetrievalConfiguration, MutableLeagueGameRetrievalConfiguration>))]
        public ILgoLeagueGameRetrievalConfiguration LeagueGameRetrievalConfiguration { get; init; } = MutableLeagueGameRetrievalConfiguration.IncludeEverything;

        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueItemRetrievalConfiguration, MutableLeagueItemRetrievalConfiguration>))]
        public ILgoLeagueItemRetrievalConfiguration LeagueItemRetrievalConfiguration { get; init; } = MutableLeagueItemRetrievalConfiguration.IncludeEverything;

        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeaguePlayerRetrievalConfiguration, MutableLeaguePlayerRetrievalConfiguration>))]
        public ILgoLeaguePlayerRetrievalConfiguration LeaguePlayerRetrievalConfiguration { get; init; } = MutableLeaguePlayerRetrievalConfiguration.IncludeEverything;

        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeaguePowerPlayTimerRetrievalConfiguration, MutableLeaguePowerPlayRetrievalConfiguration>))]
        public ILgoLeaguePowerPlayTimerRetrievalConfiguration LeaguePowerPlayTimerRetrievalConfiguration { get; init; } = MutableLeaguePowerPlayRetrievalConfiguration.IncludeEverything;

        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueTeamRetrievalConfiguration, MutableLeagueTeamRetrievalConfiguration>))]
        public ILgoLeagueTeamRetrievalConfiguration LeagueTeamRetrievalConfiguration { get; init; } = MutableLeagueTeamRetrievalConfiguration.IncludeEverything;

        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueTimerRetrievalConfiguration, MutableLeagueTimerRetrievalConfiguration>))]
        public ILgoLeagueTimerRetrievalConfiguration LeagueTimerRetrievalConfiguration { get; init; } = MutableLeagueTimerRetrievalConfiguration.IncludeEverything;
    }
}