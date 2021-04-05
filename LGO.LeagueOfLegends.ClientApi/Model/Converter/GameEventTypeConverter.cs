using LGO.Backend.Core.Model.Converter;
using LGO.LeagueOfLegends.ClientApi.Model.GameEvent;

namespace LGO.LeagueOfLegends.ClientApi.Model.Converter
{
    internal class GameEventTypeConverter : AbstractStringEnumConverter<LolClientGameEventType>
    {
        public GameEventTypeConverter(): base((LolClientGameEventType.Undefined, "UNDEFINED"),
                                          (LolClientGameEventType.GameStarted, "GameStart"),
                                          (LolClientGameEventType.FirstMinionWaveSpawned, "MinionsSpawning"),
                                          (LolClientGameEventType.FirstTurretDestroyed, "FirstBrick"),
                                          (LolClientGameEventType.TurretDestroyed, "TurretKilled"),
                                          (LolClientGameEventType.InhibitorDestroyed, "InhibKilled"),
                                          (LolClientGameEventType.InhibitorAboutToBeReconstructed, "InhibRespawningSoon"),
                                          (LolClientGameEventType.InhibitorReconstructed, "InhibRespawned"),
                                          (LolClientGameEventType.DragonKilled, "DragonKill"),
                                          (LolClientGameEventType.RiftHeraldKilled, "HeraldKill"),
                                          (LolClientGameEventType.BaronNashorKilled, "BaronKill"),
                                          (LolClientGameEventType.FirstChampionKilled, "FirstBlood"),
                                          (LolClientGameEventType.ChampionKilled, "ChampionKill"),
                                          (LolClientGameEventType.MultipleChampionsKilled, "MultiKill"),
                                          (LolClientGameEventType.EntireTeamKilled, "Ace"),
                                          (LolClientGameEventType.GameEnded, "GameEnd")) { }
        {
            
        }
    }
}