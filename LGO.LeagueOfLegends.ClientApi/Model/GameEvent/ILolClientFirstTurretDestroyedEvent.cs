namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientFirstTurretDestroyedEvent : ILolClientKillerEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.FirstTurretDestroyed;
    }
}