using LGO.LeagueResource.Model.Champion;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal sealed class NullChampionImages : ILeagueResourceChampionImages
    {
        public string FullImage => string.Empty;
        public string SpriteImage => string.Empty;

        private static NullChampionImages? _instance;

        public static NullChampionImages Instance => _instance ??= new NullChampionImages();

        private NullChampionImages()
        {
        }
    }
}