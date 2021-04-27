namespace LGO.LeagueResource.Model.Champion
{
    public interface ILeagueResourceChampion
    {
        string Id { get; }

        string Name { get; }

        ILeagueResourceChampionImages Images { get; }
    }
}