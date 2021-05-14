using Newtonsoft.Json;

namespace LGO.Backend.Model.Retrieval
{
    internal sealed record MutableLeagueTimerRetrievalConfiguration : ILgoLeagueTimerRetrievalConfiguration
    {
        public static MutableLeagueTimerRetrievalConfiguration IncludeNothing { get; } = new()
                                                                                         {
                                                                                             IncludeInGameStartTimeInSeconds = false,
                                                                                             IncludeInGameEndTimeInSeconds = false,
                                                                                         };

        public static MutableLeagueTimerRetrievalConfiguration IncludeEverything { get; } = new()
                                                                                            {
                                                                                                IncludeInGameStartTimeInSeconds = true,
                                                                                                IncludeInGameEndTimeInSeconds = true,
                                                                                            };
        
        [JsonProperty]
        public bool IncludeInGameStartTimeInSeconds { get; set; } = false;
        [JsonProperty]
        public bool IncludeInGameEndTimeInSeconds { get; set; } = false;
    }
}