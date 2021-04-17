using System;

namespace LGO.Backend.Model.League.Player
{
    public interface ILeagueChampion
    {
        Guid Id { get; }

        string Name { get; }
        
        string TileImage { get; }
        
        string SplashImage { get; }
        
        string LoadingImage { get; }
    }
}