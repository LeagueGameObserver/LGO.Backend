namespace LGO.LeagueClient.Model.GameEvent
{
    public enum LeagueClientGameEventType
    {
        Undefined,
        GameStarted,
        FirstMinionWaveSpawned,
        FirstTurretDestroyed,
        TurretDestroyed,
        InhibitorDestroyed,
        InhibitorAboutToBeReconstructed,
        InhibitorReconstructed,
        DragonKilled,
        RiftHeraldKilled,
        BaronNashorKilled,
        FirstChampionKilled,
        ChampionKilled,
        MultipleChampionsKilled,
        EntireTeamKilled,
        GameEnded,
    }
}