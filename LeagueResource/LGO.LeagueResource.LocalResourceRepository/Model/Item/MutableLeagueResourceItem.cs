using System;
using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal class MutableLeagueResourceItem : ILeagueResourceItem
    {
        public Guid Id { get; } = Guid.NewGuid();

        public int Key { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public ILeagueResourceItemImages Images { get; set; } = NullItemImages.Instance;
        
        public ILeagueResourceItemCosts Costs { get; set; } = NullItemCosts.Instance;
        
        public bool IsPurchasable { get; set; } = false;
    }
}