using LGO.Backend.Core.Model.Converter;
using LGO.LeagueApi.Model.Static.Item;
using Newtonsoft.Json;

namespace LGO.LeagueApi.RemoteApiReader.Static.Model.Item
{
    internal class MutableLeagueStaticItem : ILeagueStaticItem
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonProperty("gold")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueStaticItemCosts, MutableLeagueStaticItemCosts>))]
        public ILeagueStaticItemCosts Costs { get; set; } = NullItemCosts.Instance;
    }
}