using System.Collections.Generic;

namespace LGO.LeagueApi.Model.Static.Champion
{
    public interface ILeagueStaticChampionCollection
    {
        IReadOnlyDictionary<string, ILeagueStaticChampion> Entries { get; }
    }
}