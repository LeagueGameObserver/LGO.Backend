using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Champion;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal class MutableLeagueResourceChampionImages : ILeagueResourceChampionImages
    {
        public IImageReader SplashImage { get; set; } = NullImageReader.Instance;
        
        public IImageReader LoadingImage { get; set; } = NullImageReader.Instance;
        
        public IImageReader SquareImage { get; set; } = NullImageReader.Instance;
    }
}