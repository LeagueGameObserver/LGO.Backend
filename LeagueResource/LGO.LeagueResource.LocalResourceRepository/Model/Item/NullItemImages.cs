using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal sealed class NullItemImages : ILeagueResourceItemImages
    {
        public string FullImage => string.Empty;

        private static NullItemImages? _instance;

        public static NullItemImages Instance => _instance ??= new NullItemImages();

        private NullItemImages()
        {
        }
    }
}