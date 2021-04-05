using LGO.Backend.Core.Model.LeagueOfLegends.Enum;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal class MutableDragonKilledEvent : AbstractMutableNeutralObjectiveKilledEvent, ILolClientDragonKilledEvent
    {
        [JsonProperty("DragonType")]
        [JsonConverter(typeof(DragonTypeConverter))]
        public LolDragonType DragonType { get; set; } = LolDragonType.Undefined;
    }
}