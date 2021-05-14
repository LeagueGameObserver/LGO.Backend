using LGO.Backend.Core.Model.League.Structure;
using LGO.LeagueClient.LocalGameReader.Converter;
using LGO.LeagueClient.Model.GameEvent;
using Newtonsoft.Json;

namespace LGO.LeagueClient.LocalGameReader.Model.GameEvent
{
    internal class MutableLeagueClientTurretDestroyedEvent : AbstractMutableLeagueClientKillerWithAssistersEvent, ILeagueClientTurretDestroyedEvent
    {
        public ILeagueTurret Turret { get; set; } = NullLeagueTurret.Instance;

        [JsonProperty("TurretKilled")]
        public string TurretLeagueClientString { get; set; } = string.Empty;
    }
}