using LGO.Backend.Core.Model.Converter;
using LGO.LeagueResource.Model.Champion;
using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal class MutableLeagueResourceChampion : ILeagueResourceChampion
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("image")]
        [JsonConverter(typeof(ConcreteConverter<ILeagueResourceChampionImages, MutableLeagueResourceChampionImages>))]
        public ILeagueResourceChampionImages Images { get; set; } = MutableLeagueResourceChampionImages.Null;
    }
}