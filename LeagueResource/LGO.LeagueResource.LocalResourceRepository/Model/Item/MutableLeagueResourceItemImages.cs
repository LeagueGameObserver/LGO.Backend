using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal class MutableLeagueResourceItemImages : ILeagueResourceItemImages
    {
        public string FullImage { get; set; } = string.Empty;
        
        public static ILeagueResourceItemImages Null => NullItemImages.Instance;
    }
}