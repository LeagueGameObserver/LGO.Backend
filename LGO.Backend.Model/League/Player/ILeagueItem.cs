namespace LGO.Backend.Model.League.Player
{
    public interface ILeagueItem
    {
        int Id { get; }
        
        string? Name { get; }
        
        int? Amount { get; }
        
        int? Price { get; }
        
        string? ImageUrl { get; }
    }
}