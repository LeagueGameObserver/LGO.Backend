using Newtonsoft.Json;

namespace LGO.Backend.Model.Retrieval
{
    internal sealed record MutableLeagueItemRetrievalConfiguration : ILgoLeagueItemRetrievalConfiguration
    {
        public static MutableLeagueItemRetrievalConfiguration IncludeNothing { get; } = new()
                                                                                     {
                                                                                         IncludeName = false,
                                                                                         IncludeAmount = false,
                                                                                         IncludePrice = false,
                                                                                         IncludeImage = false,
                                                                                     };

        public static MutableLeagueItemRetrievalConfiguration IncludeEverything { get; } = new()
                                                                                        {
                                                                                            IncludeName = true,
                                                                                            IncludeAmount = true,
                                                                                            IncludePrice = true,
                                                                                            IncludeImage = true,
                                                                                        };

        [JsonProperty]
        public bool IncludeName { get; set; } = false;

        [JsonProperty]
        public bool IncludeAmount { get; set; } = false;
        [JsonProperty]
        public bool IncludePrice { get; set; } = false;
        [JsonProperty]
        public bool IncludeImage { get; set; } = false;
    }
}