using Newtonsoft.Json;

namespace LGO.Backend.Model.Retrieval
{
    internal sealed record MutableLeagueTeamRetrievalConfiguration : ILgoLeagueTeamRetrievalConfiguration
    {
        public static MutableLeagueTeamRetrievalConfiguration IncludeNothing { get; } = new()
                                                                                        {
                                                                                            IncludeSide = false,
                                                                                            IncludeDragonsKilled = false,
                                                                                            IncludeNumberOfRiftHeraldsKilled = false,
                                                                                            IncludeNumberOfBaronNashorsKilled = false,
                                                                                            IncludeTurretsDestroyed = false,
                                                                                            IncludeInhibitorsDestroyed = false,
                                                                                        };
        
        public static MutableLeagueTeamRetrievalConfiguration IncludeEverything { get; } = new()
                                                                                        {
                                                                                            IncludeSide = true,
                                                                                            IncludeDragonsKilled = true,
                                                                                            IncludeNumberOfRiftHeraldsKilled = true,
                                                                                            IncludeNumberOfBaronNashorsKilled = true,
                                                                                            IncludeTurretsDestroyed = true,
                                                                                            IncludeInhibitorsDestroyed = true,
                                                                                        };
        
        [JsonProperty]
        public bool IncludeSide { get; set; } = false;
        [JsonProperty]
        public bool IncludeDragonsKilled { get; set; } = false;
        [JsonProperty]
        public bool IncludeNumberOfRiftHeraldsKilled { get; set; } = false;
        [JsonProperty]
        public bool IncludeNumberOfBaronNashorsKilled { get; set; } = false;
        [JsonProperty]
        public bool IncludeTurretsDestroyed { get; set; } = false;
        [JsonProperty]
        public bool IncludeInhibitorsDestroyed { get; set; } = false;
    }
}