using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Player.Internal
{
    internal class MutableScore : ILolClientScore
    {
        [JsonProperty("kills")]
        public int Kills { get; set; }
        
        [JsonProperty("deaths")]
        public int Deaths { get; set; }
        
        [JsonProperty("assists")]
        public int Assists { get; set; }
        
        [JsonProperty("creepScore")]
        public int MinionKills { get; set; }
        
        [JsonProperty("wardScore")]
        public double Vision { get; set; }
    }
}