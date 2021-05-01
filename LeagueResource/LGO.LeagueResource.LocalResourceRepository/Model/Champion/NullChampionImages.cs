using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Champion;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal sealed class NullChampionImages : ILeagueResourceChampionImages
    {
        private static NullChampionImages? _instance;

        public static NullChampionImages Instance => _instance ??= new NullChampionImages();

        public IImageReader SplashImage => NullImageReader.Instance;
        
        public IImageReader LoadingImage => NullImageReader.Instance;
        
        public IImageReader SquareImage => NullImageReader.Instance;

        private NullChampionImages()
        {
        }
    }
}