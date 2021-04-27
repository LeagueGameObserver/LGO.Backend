using LGO.LeagueResource.Model.Item;
using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal class MutableLeagueResourceItemImages : ILeagueResourceItemImages
    {
        [JsonProperty("full")]
        public string FullImage { get; set; } = string.Empty;
        
        public static ILeagueResourceItemImages Null => NullItemImages.Instance;
    }
}