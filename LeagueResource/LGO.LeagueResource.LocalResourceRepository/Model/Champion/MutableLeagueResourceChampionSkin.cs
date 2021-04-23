using LGO.LeagueResource.Model.Champion;
using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal class MutableLeagueResourceChampionSkin : ILeagueResourceChampionSkin
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("num")]
        public int Index { get; set; } = 0;
    }
}