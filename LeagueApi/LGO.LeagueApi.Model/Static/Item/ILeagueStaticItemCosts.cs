namespace LGO.LeagueApi.Model.Static.Item
{
    public interface ILeagueStaticItemCosts
    {
        int TotalCosts { get; }
        
        int RecipeCosts { get; }
        
        int SellWorth { get; }
        
        bool IsPurchasable { get; }
    }
}