namespace LGO.LeagueApi.Model.Static.Item
{
    public interface ILeagueStaticItem
    {
        string Name { get; }
        
        ILeagueStaticItemCosts Costs { get; }
    }
}