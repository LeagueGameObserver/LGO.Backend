namespace LGO.LeagueResource.Model.Item
{
    public interface ILeagueResourceItemCosts
    {
        int TotalCosts { get; }
        
        int RecipeCosts { get; }
        
        int SellWorth { get; }
        
        bool CanBePurchased { get; }
    }
}