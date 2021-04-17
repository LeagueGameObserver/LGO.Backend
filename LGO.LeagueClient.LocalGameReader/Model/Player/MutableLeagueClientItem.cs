using LGO.LeagueClient.Model.Player;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.Player
{
    internal class MutableLeagueClientItem : ILeagueClientItem
    {
        [JsonProperty("itemID")]
        public int Id { get; set; }
        
        [JsonProperty("count")]
        public int Amount { get; set; }
    }
}