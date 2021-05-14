using System;

namespace LGO.Backend.Model.League.Player
{
    internal sealed class MutableLeagueChampion : ILeagueChampion
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string? Name { get; set; } = null;
        public string? TileImage { get; set; } = null;
        public string? SplashImage { get; set; } = null;
        public string? LoadingImage { get; set; } = null;
    }
}