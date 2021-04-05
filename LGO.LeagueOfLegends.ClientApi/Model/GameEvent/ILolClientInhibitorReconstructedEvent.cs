namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientInhibitorReconstructedEvent : ILolClientInhibitorEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.InhibitorReconstructed;
    }
}