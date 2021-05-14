using Newtonsoft.Json;

namespace LGO.Backend.Model.Retrieval
{
    internal sealed record MutableLeaguePowerPlayRetrievalConfiguration : ILgoLeaguePowerPlayTimerRetrievalConfiguration
    {
        public static MutableLeaguePowerPlayRetrievalConfiguration IncludeNothing { get; } = new()
                                                                                             {
                                                                                                 IncludeInGameStartTimeInSeconds = false,
                                                                                                 IncludeInGameEndTimeInSeconds = false,
                                                                                                 IncludeIsActive = false,
                                                                                                 IncludeMatchUps = false,
                                                                                             };

        public static MutableLeaguePowerPlayRetrievalConfiguration IncludeEverything { get; } = new()
                                                                                                {
                                                                                                    IncludeInGameStartTimeInSeconds = true,
                                                                                                    IncludeInGameEndTimeInSeconds = true,
                                                                                                    IncludeIsActive = true,
                                                                                                    IncludeMatchUps = true,
                                                                                                };

        [JsonProperty]
        public bool IncludeInGameStartTimeInSeconds { get; set; } = false;

        [JsonProperty]
        public bool IncludeInGameEndTimeInSeconds { get; set; } = false;

        [JsonProperty]
        public bool IncludeIsActive { get; set; } = false;

        [JsonProperty]
        public bool IncludeMatchUps { get; set; } = false;
    }
}