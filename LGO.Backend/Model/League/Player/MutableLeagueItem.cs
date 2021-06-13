namespace LGO.Backend.Model.League.Player
{
    internal sealed class MutableLeagueItem : ILeagueItem
    {
        public int Id { get; set; } = 0;
        public string? Name { get; set; } = null;
        public int? Amount { get; set; } = null;
        public int? Price { get; set; } = null;
        public string? ImageUrl { get; set; } = null;
    }
}