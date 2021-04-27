using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Champion;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal class MutableLeagueResourceChampionImages : ILeagueResourceChampionImages
    {
        public IImageDescriptor SplashImage { get; set; } = NullImageDescriptor.Instance;
        
        public IImageDescriptor LoadingImage { get; set; } = NullImageDescriptor.Instance;
        
        public IImageDescriptor SquareImage { get; set; } = NullImageDescriptor.Instance;
    }
}