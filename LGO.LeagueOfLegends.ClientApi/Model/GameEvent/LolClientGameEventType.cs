namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public enum LolClientGameEventType
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