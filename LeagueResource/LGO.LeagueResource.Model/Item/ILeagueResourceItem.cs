namespace LGO.LeagueResource.Model.Item
{
    public interface ILeagueResourceItem
    {
        int Id { get; }
        
        string Name { get; }
        
        ILeagueResourceItemCosts Costs { get; }
        
        ILeagueResourceItemImages Images { get; }
    }
}