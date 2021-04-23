using LGO.LeagueResource.Model.Item;
using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal class MutableLeagueResourceItemCosts : ILeagueResourceItemCosts
    {
        [JsonProperty("total")]
        public int TotalCosts { get; set; }
        
        [JsonProperty("base")]
        public int RecipeCosts { get; set; }
        
        [JsonProperty("sell")]
        public int SellWorth { get; set; }
        
        [JsonProperty("purchasable")]
        public bool CanBePurchased { get; set; }
        
        public static ILeagueResourceItemCosts Null => NullItemCosts.Instance;
    }
}