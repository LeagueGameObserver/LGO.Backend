using LGO.LeagueResource.Model.Champion;
using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal class MutableLeagueResourceChampionImages : ILeagueResourceChampionImages
    {
        [JsonProperty("full")]
        public string FullImage { get; set; } = string.Empty;

        [JsonProperty("sprite")]
        public string SpriteImage { get; set; } = string.Empty;
        
        public static ILeagueResourceChampionImages Null => NullChampionImages.Instance;
    }
}