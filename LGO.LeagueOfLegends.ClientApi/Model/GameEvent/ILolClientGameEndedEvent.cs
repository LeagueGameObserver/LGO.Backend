namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientGameEndedEvent : ILolClientGameEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.GameEnded;
    }
}