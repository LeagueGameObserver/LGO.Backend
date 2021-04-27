using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal class MutableLeagueResourceItemCosts : ILeagueResourceItemCosts
    {
        public int TotalCosts { get; set; } = 0;

        public int RecipeCosts { get; set; } = 0;

        public int SellWorth { get; set; } = 0;
    }
}