using LGO.LeagueClient.Model.GameEvent;

namespace LGO.LeagueClient.LocalGameReader.Converter
{
    internal class GameEventTypeConverter : ReadonlyStringEnumConverter<LeagueClientGameEventType>
    {
        public GameEventTypeConverter() : base(LeagueClientGameEventType.Undefined,
                                               ("GameStart", LeagueClientGameEventType.GameStarted),
                                               ("MinionsSpawning", LeagueClientGameEventType.FirstMinionWaveSpawned),
                                               ("FirstBrick", LeagueClientGameEventType.FirstTurretDestroyed),
                                               ("TurretKilled", LeagueClientGameEventType.TurretDestroyed),
                                               ("InhibKilled", LeagueClientGameEventType.InhibitorDestroyed),
                                               ("InhibRespawningSoon", LeagueClientGameEventType.InhibitorAboutToBeReconstructed),
                                               ("InhibRespawned", LeagueClientGameEventType.InhibitorReconstructed),
                                               ("DragonKill", LeagueClientGameEventType.DragonKilled),
                                               ("HeraldKill", LeagueClientGameEventType.DragonKilled),
                                               ("BaronKill", LeagueClientGameEventType.BaronNashorKilled),
                                               ("FirstBlood", LeagueClientGameEventType.FirstChampionKilled),
                                               ("ChampionKill", LeagueClientGameEventType.ChampionKilled),
                                               ("MultiKill", LeagueClientGameEventType.MultipleChampionsKilled),
                                               ("Ace", LeagueClientGameEventType.EntireTeamKilled),
                                               ("GameEnd", LeagueClientGameEventType.GameEnded))
        {
        }
    }
}