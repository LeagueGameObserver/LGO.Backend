using System.Collections.Generic;
using LGO.Backend.Core.Model.Converter;
using LGO.LeagueApi.Model.Static.Champion;
using Newtonsoft.Json;

namespace LGO.LeagueApi.RemoteApiReader.Static.Model.Champion
{
    internal class MutableLeagueStaticChampionCollection : ILeagueStaticChampionCollection
    {
        [JsonProperty("data", ItemConverterType = typeof(ConcreteConverter<ILeagueStaticChampion, MutableLeagueStaticChampion>))]
        public IReadOnlyDictionary<string, ILeagueStaticChampion> Entries { get; set; } = new Dictionary<string, ILeagueStaticChampion>();
    }
}