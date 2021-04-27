using System;

namespace LGO.LeagueResource.Model.Item
{
    public interface ILeagueResourceItem
    {
        Guid Id { get; }
        
        int Key { get; }
        
        string Name { get; }
        
        ILeagueResourceItemImages Images { get; }
        
        ILeagueResourceItemCosts Costs { get; }
        
        bool IsPurchasable { get; }
    }
}