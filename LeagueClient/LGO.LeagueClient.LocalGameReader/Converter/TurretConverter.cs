using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Core.Model.League.Structure;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal static class TurretConverter
    {
        private const string TokenSeparator = "_";

        private static BidirectionalStringMapping<LeagueTeamType> TeamMapping { get; } = new((LeagueTeamType.Undefined, "UNDEFINED"),
                                                                                             (LeagueTeamType.Blue, "T1"),
                                                                                             (LeagueTeamType.Red, "T2"));

        public static ILeagueTurret CreateTurret(string clientTurretString, LeagueMapType map)
        {
            return map switch
                   {
                       LeagueMapType.SummonersRift => CreateTurretOnSummonersRift(clientTurretString),
                       LeagueMapType.HowlingAbyss => CreateTurretOnHowlingAbyss(clientTurretString),
                       _ => NullLeagueTurret.Instance
                   };
        }

        private static ILeagueTurret CreateTurretOnSummonersRift(string clientTurretString)
        {
            var tokens = clientTurretString.Split(TokenSeparator);
            var team = TeamMapping.Get(tokens[1]);

            if (team == LeagueTeamType.Undefined)
            {
                return NullLeagueTurret.Instance;
            }

            var tier = string.Join(TokenSeparator, tokens[2], tokens[3]) switch
                       {
                           "L_03" => LeagueTurretTierType.TopOuter,
                           "L_02" => LeagueTurretTierType.TopInner,
                           "L_01" => LeagueTurretTierType.TopInhibitor,
                           "C_05" => LeagueTurretTierType.MiddleOuter,
                           "C_04" => LeagueTurretTierType.MiddleInner,
                           "C_03" => LeagueTurretTierType.MiddleInhibitor,
                           "C_02" => LeagueTurretTierType.NexusBottom,
                           "C_01" => LeagueTurretTierType.NexusTop,
                           "R_03" => LeagueTurretTierType.BottomOuter,
                           "R_02" => LeagueTurretTierType.BottomInner,
                           "R_01" => LeagueTurretTierType.BottomInhibitor,
                           _ => LeagueTurretTierType.Undefined,
                       };

            return new MutableLeagueTurret
                   {
                       Team = team,
                       Tier = tier
                   };
        }

        private static ILeagueTurret CreateTurretOnHowlingAbyss(string clientTurretString)
        {
            var tokens = clientTurretString.Split(TokenSeparator);
            var team = TeamMapping.Get(tokens[1]);

            if (team == LeagueTeamType.Undefined)
            {
                return NullLeagueTurret.Instance;
            }

            var tierToken = string.Join(TokenSeparator, tokens[2], tokens[3]);
            var tier = team switch
                       {
                           LeagueTeamType.Blue => tierToken switch
                                                  {
                                                      "C_08" => LeagueTurretTierType.MiddleInner,
                                                      "C_07" => LeagueTurretTierType.MiddleInhibitor,
                                                      "C_09" => LeagueTurretTierType.NexusBottom,
                                                      "C_010" => LeagueTurretTierType.NexusTop,
                                                      _ => LeagueTurretTierType.Undefined
                                                  },
                           LeagueTeamType.Red => tierToken switch
                                                 {
                                                     "L_01" => LeagueTurretTierType.MiddleInner,
                                                     "L_02" => LeagueTurretTierType.MiddleInhibitor,
                                                     "L_03" => LeagueTurretTierType.NexusBottom,
                                                     "L_04" => LeagueTurretTierType.NexusTop,
                                                     _ => LeagueTurretTierType.Undefined,
                                                 },
                           _ => LeagueTurretTierType.Undefined,
                       };

            return new MutableLeagueTurret
                   {
                       Team = team,
                       Tier = tier
                   };
        }
    }
}