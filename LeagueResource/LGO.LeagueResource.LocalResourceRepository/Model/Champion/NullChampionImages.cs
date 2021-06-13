using System;
using LGO.LeagueResource.Model.Champion;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal sealed class NullChampionImages : ILeagueResourceChampionImages
    {
        private static Uri NullUri { get; } = new("http://null.null");
        
        private static NullChampionImages? _instance;

        public static NullChampionImages Instance => _instance ??= new NullChampionImages();

        public Uri SplashImage => NullUri;

        public Uri LoadingImage => NullUri;
        
        public Uri SquareImage => NullUri;

        private NullChampionImages()
        {
        }
    }
}