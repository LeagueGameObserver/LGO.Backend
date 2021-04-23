using System.Collections.Generic;
using System.Linq;
using LGO.Backend.Core.Model.Converter;
using LGO.LeagueResource.Model.Champion;
using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal class MutableLeagueResourceChampion : ILeagueResourceChampion
    {
        [JsonProperty("key")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("image")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueResourceChampionImages, MutableLeagueResourceChampionImages>))]
        public ILeagueResourceChampionImages Images { get; set; } = MutableLeagueResourceChampionImages.Null;
        
        [JsonProperty("skins", ItemConverterType = typeof(ConcreteConverter<ILeagueResourceChampionSkin, MutableLeagueResourceChampionSkin>))]
        public IEnumerable<ILeagueResourceChampionSkin> Skins { get; set; } = Enumerable.Empty<ILeagueResourceChampionSkin>();
    }
}