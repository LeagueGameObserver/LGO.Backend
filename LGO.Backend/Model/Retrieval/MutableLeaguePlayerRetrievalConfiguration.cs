using Newtonsoft.Json;

namespace LGO.Backend.Model.Retrieval
{
    internal sealed record MutableLeaguePlayerRetrievalConfiguration : ILgoLeaguePlayerRetrievalConfiguration
    {
        public static MutableLeaguePlayerRetrievalConfiguration IncludeNothing { get; } = new()
                                                                                       {
                                                                                           IncludeSummonerName = false,
                                                                                           IncludeTeam = false,
                                                                                           IncludeChampion = false,
                                                                                           IncludeItems = false,
                                                                                       };
        
        public static MutableLeaguePlayerRetrievalConfiguration IncludeEverything { get; } = new()
                                                                                       {
                                                                                           IncludeSummonerName = true,
                                                                                           IncludeTeam = true,
                                                                                           IncludeChampion = true,
                                                                                           IncludeItems = true,
                                                                                       };
        
        [JsonProperty]
        public bool IncludeSummonerName { get; set; } = false;
        [JsonProperty]
        public bool IncludeTeam { get; set; } = false;
        [JsonProperty]
        public bool IncludeChampion { get; set; } = false;
        [JsonProperty]
        public bool IncludeItems { get; set; } = false;
    }
}