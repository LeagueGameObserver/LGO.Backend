using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal sealed class NullItemImages : ILeagueResourceItemImages
    {
        private static NullItemImages? _instance;

        public static NullItemImages Instance => _instance ??= new NullItemImages();

        public IImageReader SquareImage => NullImageReader.Instance;

        private NullItemImages()
        {
        }
    }
}