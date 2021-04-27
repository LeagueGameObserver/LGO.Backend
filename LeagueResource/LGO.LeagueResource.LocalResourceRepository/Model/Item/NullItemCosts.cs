using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal sealed class NullItemCosts : ILeagueResourceItemCosts
    {
        private static NullItemCosts? _instance;

        public static NullItemCosts Instance => _instance ??= new NullItemCosts();

        public int TotalCosts => 0;

        public int RecipeCosts => 0;

        public int SellWorth => 0;

        private NullItemCosts()
        {
        }
    }
}