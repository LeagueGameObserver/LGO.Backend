using LGO.Backend.Core.Model.Converter;
using LGO.LeagueResource.Model.Item;
using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal class MutableLeagueResourceItem : ILeagueResourceItem
    {
        public int Id { get; set; } = 0;
        
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        
        [JsonProperty("gold")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueResourceItemCosts, MutableLeagueResourceItemCosts>))]
        public ILeagueResourceItemCosts Costs { get; set; } = MutableLeagueResourceItemCosts.Null;
        
        [JsonProperty("image")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueResourceItemImages, MutableLeagueResourceItemImages>))]
        public ILeagueResourceItemImages Images { get; set; } = MutableLeagueResourceItemImages.Null;
    }
}