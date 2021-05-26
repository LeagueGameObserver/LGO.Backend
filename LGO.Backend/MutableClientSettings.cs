using LGO.Backend.Core.Model.Converter;
using LGO.Backend.Model;
using LGO.Backend.Model.Retrieval;
using Newtonsoft.Json;

namespace LGO.Backend
{
    public class MutableClientSettings : ILgoClientSettings
    {
        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueChampionRetrievalConfiguration, MutableLeagueChampionRetrievalConfiguration>))]
        public ILgoLeagueChampionRetrievalConfiguration LeagueChampionRetrievalConfiguration { get; set; } = MutableLeagueChampionRetrievalConfiguration.IncludeEverything;
        
        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueGameRetrievalConfiguration, MutableLeagueGameRetrievalConfiguration>))]
        public ILgoLeagueGameRetrievalConfiguration LeagueGameRetrievalConfiguration { get; set; } = MutableLeagueGameRetrievalConfiguration.IncludeEverything;
        
        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueItemRetrievalConfiguration, MutableLeagueItemRetrievalConfiguration>))]
        public ILgoLeagueItemRetrievalConfiguration LeagueItemRetrievalConfiguration { get; set; } = MutableLeagueItemRetrievalConfiguration.IncludeEverything;
        
        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeaguePlayerRetrievalConfiguration, MutableLeaguePlayerRetrievalConfiguration>))]
        public ILgoLeaguePlayerRetrievalConfiguration LeaguePlayerRetrievalConfiguration { get; set; } = MutableLeaguePlayerRetrievalConfiguration.IncludeEverything;
        
        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeaguePowerPlayTimerRetrievalConfiguration, MutableLeaguePowerPlayRetrievalConfiguration>))]
        public ILgoLeaguePowerPlayTimerRetrievalConfiguration LeaguePowerPlayTimerRetrievalConfiguration { get; set; } = MutableLeaguePowerPlayRetrievalConfiguration.IncludeEverything;
        
        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueTeamRetrievalConfiguration, MutableLeagueTeamRetrievalConfiguration>))]
        public ILgoLeagueTeamRetrievalConfiguration LeagueTeamRetrievalConfiguration { get; set; } = MutableLeagueTeamRetrievalConfiguration.IncludeEverything;
        
        [JsonProperty]
        [JsonConverter(typeof(ConcreteConverter<ILgoLeagueTimerRetrievalConfiguration, MutableLeagueTimerRetrievalConfiguration>))]
        public ILgoLeagueTimerRetrievalConfiguration LeagueTimerRetrievalConfiguration { get; set; } = MutableLeagueTimerRetrievalConfiguration.IncludeEverything;
    }
}