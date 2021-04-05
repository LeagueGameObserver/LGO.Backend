using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.ActivePlayer.Internal
{
    internal class MutableActivePlayer : ILolClientActivePlayer
    {
        [JsonProperty("summonerName")]
        public string SummonerName { get; set; } = string.Empty;
    }
}