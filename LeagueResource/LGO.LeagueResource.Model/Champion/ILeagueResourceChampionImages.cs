using System;

namespace LGO.LeagueResource.Model.Champion
{
    public interface ILeagueResourceChampionImages
    {
        Uri SplashImage { get; }
        
        Uri LoadingImage { get; }
        
        Uri SquareImage { get; }
    }
}