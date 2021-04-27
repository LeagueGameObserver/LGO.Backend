using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Champion;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal sealed class NullChampionImages : ILeagueResourceChampionImages
    {
        private static NullChampionImages? _instance;

        public static NullChampionImages Instance => _instance ??= new NullChampionImages();

        public IImageDescriptor SplashImage => NullImageDescriptor.Instance;
        
        public IImageDescriptor LoadingImage => NullImageDescriptor.Instance;
        
        public IImageDescriptor SquareImage => NullImageDescriptor.Instance;

        private NullChampionImages()
        {
        }
    }
}