using Newtonsoft.Json;

namespace LGO.Backend.Model.Retrieval
{
    internal sealed record MutableLeagueChampionRetrievalConfiguration : ILgoLeagueChampionRetrievalConfiguration
    {
        public static MutableLeagueChampionRetrievalConfiguration IncludeNothing { get; } = new()
                                                                                                {
                                                                                                    IncludeName = false,
                                                                                                    IncludeTitleImage = false,
                                                                                                    IncludeSplashImage = false,
                                                                                                    IncludeLoadingImage = false,
                                                                                                };

        public static MutableLeagueChampionRetrievalConfiguration IncludeEverything { get; } = new()
                                                                                                  {
                                                                                                      IncludeName = true,
                                                                                                      IncludeTitleImage = true,
                                                                                                      IncludeSplashImage = true,
                                                                                                      IncludeLoadingImage = true,
                                                                                                  };
        
        [JsonProperty]
        public bool IncludeName { get; set; } = false;
        [JsonProperty]
        public bool IncludeTitleImage { get; set; } = false;
        [JsonProperty]
        public bool IncludeSplashImage { get; set; } = false;
        [JsonProperty]
        public bool IncludeLoadingImage { get; set; } = false;
    }
}