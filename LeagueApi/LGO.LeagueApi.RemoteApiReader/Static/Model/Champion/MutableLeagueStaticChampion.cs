using LGO.LeagueApi.Model.Static.Champion;
using Newtonsoft.Json;

namespace LGO.LeagueApi.RemoteApiReader.Static.Model.Champion
{
    internal class MutableLeagueStaticChampion : ILeagueStaticChampion
    {
        [JsonProperty("id")]
        public string Id { get; set; } = string.Empty;
        
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}