using System;
using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Champion;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal class MutableLeagueResourceChampionImages : ILeagueResourceChampionImages
    {
        public Uri SplashImage { get; set; } = null!;

        public Uri LoadingImage { get; set; } = null!;

        public Uri SquareImage { get; set; } = null!;
    }
}