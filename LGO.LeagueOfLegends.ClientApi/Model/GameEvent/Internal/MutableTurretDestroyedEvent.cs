using LGO.Backend.Core.Model.LeagueOfLegends.Structure;
using LGO.LeagueOfLegends.ClientApi.Model.Converter;
using LGO.LeagueOfLegends.ClientApi.Model.Structure.Internal;
using Newtonsoft.Json;

namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent.Internal
{
    internal class MutableTurretDestroyedEvent : AbstractMutableKillerWithAssistersEvent, ILolClientTurretDestroyedEvent
    {
        [JsonProperty("TurretKilled")]
        [JsonConverter(typeof(TurretConverter))]
        public ILolTurret Turret { get; set; } = NullTurret.Get;
    }
}