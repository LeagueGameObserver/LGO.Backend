using LGO.LeagueClient.Model.Player;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.Player
{
    internal class MutableLeagueClientScore : ILeagueClientScore
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
        
        public static ILeagueClientScore Null => NullScore.Get;
    }
}