using System.Collections.Generic;
using LGO.Backend.Core.Model.Converter;
using LGO.LeagueApi.Model.Static.Item;
using Newtonsoft.Json;

namespace LGO.LeagueApi.RemoteApiReader.Static.Model.Item
{
    internal class MutableLeagueStaticItemCollection : ILeagueStaticItemCollection
    {
        [JsonProperty("data", ItemConverterType = typeof(ConcreteConverter<ILeagueStaticItem, MutableLeagueStaticItem>))]
        public IReadOnlyDictionary<string, ILeagueStaticItem> Entries { get; set; } = new Dictionary<string, ILeagueStaticItem>();
    }
}