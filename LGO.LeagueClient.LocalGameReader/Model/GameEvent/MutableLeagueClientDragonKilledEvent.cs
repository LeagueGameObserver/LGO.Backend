using LGO.Backend.Core.Model.League.Enum;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal class MutableLeagueClientDragonKilledEvent : AbstractMutableLeagueClientNeutralObjectiveKilledEvent, ILeagueClientDragonKilledEvent
    {
        [JsonProperty("DragonType")]
        [JsonConverter(typeof(DragonTypeConverter))]
        public LeagueDragonType DragonType { get; set; } = LeagueDragonType.Undefined;
    }
}