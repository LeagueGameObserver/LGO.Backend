using LGO.LeagueClient.Model.ActivePlayer;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.ActivePlayer
{
    internal class MutableLeagueClientActivePlayer : ILeagueClientActivePlayer
    {
        [JsonProperty("summonerName")]
        public string SummonerName { get; set; } = string.Empty;
        
        public static ILeagueClientActivePlayer Null => NullActivePlayer.Get;
    }
}