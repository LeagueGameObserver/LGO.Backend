using LGO.LeagueApi.Model.Static.Item;
using Newtonsoft.Json;

namespace LGO.LeagueApi.RemoteApiReader.Static.Model.Item
{
    internal class MutableLeagueStaticItemCosts : ILeagueStaticItemCosts
    {
        [JsonProperty("total")]
        public int TotalCosts { get; set; }
        
        [JsonProperty("base")]
        public int RecipeCosts { get; set; }
        
        [JsonProperty("sell")]
        public int SellWorth { get; set; }
        
        [JsonProperty("purchasable")]
        public bool IsPurchasable { get; set; }
    }
}