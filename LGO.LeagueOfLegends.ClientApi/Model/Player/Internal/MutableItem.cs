using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.Player.Internal
{
    internal class MutableItem : ILolClientItem
    {
        [JsonProperty("itemID")]
        public int Id { get; set; }
        
        [JsonProperty("count")]
        public int Amount { get; set; }
    }
}