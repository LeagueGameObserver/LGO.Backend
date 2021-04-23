using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal sealed class NullItemCosts : ILeagueResourceItemCosts
    {
        public int TotalCosts => 0;
        public int RecipeCosts => 0;
        public int SellWorth => 0;
        public bool CanBePurchased => false;

        private static NullItemCosts? _instance;

        public static NullItemCosts Instance => _instance ??= new NullItemCosts();

        private NullItemCosts()
        {
        }
    }
}