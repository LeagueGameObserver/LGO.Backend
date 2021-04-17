using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Core.Model.League.Structure
{
    public sealed class MutableLeagueTurret : ILeagueTurret
    {
        public LeagueTurretTierType Tier { get; set; } = LeagueTurretTierType.Undefined;
        public LeagueTeamType Team { get; set; } = LeagueTeamType.Undefined;
        
        public static ILeagueTurret Null => NullLeagueTurret.Get;
    }
}