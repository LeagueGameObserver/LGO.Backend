using Newtonsoft.Json;

namespace LGO.Backend.Model.Retrieval
{
    internal sealed record MutableLeagueGameRetrievalConfiguration : ILgoLeagueGameRetrievalConfiguration
    {
        public static MutableLeagueGameRetrievalConfiguration IncludeNothing { get; } = new()
                                                                                           {
                                                                                               IncludeInGameTimeInSeconds = false,
                                                                                               IncludeMode = false,
                                                                                               IncludeTeams = false,
                                                                                               IncludePlayers = false,
                                                                                               IncludeMatchUps = false,
                                                                                               IncludeTimers = false,
                                                                                               IncludeEvents = false,
                                                                                               IncludeEventsSinceLastUpdate = false,
                                                                                           };

        public static MutableLeagueGameRetrievalConfiguration IncludeEverything { get; } = new()
                                                                                              {
                                                                                                  IncludeInGameTimeInSeconds = true,
                                                                                                  IncludeMode = true,
                                                                                                  IncludeTeams = true,
                                                                                                  IncludePlayers = true,
                                                                                                  IncludeMatchUps = true,
                                                                                                  IncludeTimers = true,
                                                                                                  IncludeEvents = true,
                                                                                                  IncludeEventsSinceLastUpdate = true,
                                                                                              };

        [JsonProperty]
        public bool IncludeInGameTimeInSeconds { get; set; } = false;
        [JsonProperty]
        public bool IncludeMode { get; set; } = false;
        [JsonProperty]
        public bool IncludeTeams { get; set; } = false;
        [JsonProperty]
        public bool IncludePlayers { get; set; } = false;
        [JsonProperty]
        public bool IncludeMatchUps { get; set; } = false;
        [JsonProperty]
        public bool IncludeTimers { get; set; } = false;
        [JsonProperty]
        public bool IncludeEvents { get; set; } = false;
        [JsonProperty]
        public bool IncludeEventsSinceLastUpdate { get; set; } = false;
    }
}