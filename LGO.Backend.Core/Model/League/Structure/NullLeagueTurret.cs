using LGO.Backend.Core.Model.League.Enum;

namespace LGO.Backend.Core.Model.League.Structure
{
    public sealed class NullLeagueTurret : ILeagueTurret
    {
        public LeagueTurretTierType Tier => LeagueTurretTierType.Undefined;
        public LeagueTeamType Team => LeagueTeamType.Undefined;

        private static NullLeagueTurret? _instance;

        public static NullLeagueTurret Instance => _instance ??= new NullLeagueTurret();
        
        private NullLeagueTurret() { }
    }
}