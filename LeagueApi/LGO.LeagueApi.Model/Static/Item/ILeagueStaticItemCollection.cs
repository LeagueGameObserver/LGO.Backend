using System.Collections.Generic;

namespace LGO.LeagueApi.Model.Static.Item
{
    public interface ILeagueStaticItemCollection
    {
        IReadOnlyDictionary<string, ILeagueStaticItem> Entries { get; }
    }
}