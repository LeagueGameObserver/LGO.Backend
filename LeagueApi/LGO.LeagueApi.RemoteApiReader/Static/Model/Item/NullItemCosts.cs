using LGO.LeagueApi.Model.Static.Item;

namespace LGO.LeagueApi.RemoteApiReader.Static.Model.Item
{
    internal sealed class NullItemCosts : ILeagueStaticItemCosts
    {
        public int TotalCosts => 0;
        public int RecipeCosts => 0;
        public int SellWorth => 0;
        public bool IsPurchasable => false;

        private static NullItemCosts? _instance;

        public static NullItemCosts Instance => _instance ??= new NullItemCosts();

        private NullItemCosts()
        {
        }
    }
}