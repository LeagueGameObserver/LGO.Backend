namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientGameStartedEvent : ILolClientGameEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.GameStarted;
    }
}