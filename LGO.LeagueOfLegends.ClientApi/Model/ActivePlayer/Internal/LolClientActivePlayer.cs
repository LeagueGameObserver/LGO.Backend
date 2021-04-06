using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.ActivePlayer.Internal
{
    internal class LolClientActivePlayer : ILolClientActivePlayer
    {
        [JsonProperty("summonerName")]
        public string SummonerName { get; set; } = string.Empty;

        [JsonProperty("currentGold")]
        public double CurrentGold { get; set; }
    }
}