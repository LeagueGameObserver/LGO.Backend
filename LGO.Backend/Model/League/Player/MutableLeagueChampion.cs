using System;

namespace LGO.Backend.Model.League.Player
{
    internal sealed class MutableLeagueChampion : ILeagueChampion
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string? Name { get; set; } = null;
        public string? TileImageUrl { get; set; } = null;
        public string? SplashImageUrl { get; set; } = null;
        public string? LoadingImageUrl { get; set; } = null;
    }
}