using System;

namespace LGO.Backend.Model.League.Player
{
    public interface ILeagueChampion
    {
        Guid Id { get; }

        string? Name { get; }
        
        string? TileImageUrl { get; }
        
        string? SplashImageUrl { get; }
        
        string? LoadingImageUrl { get; }
    }
}