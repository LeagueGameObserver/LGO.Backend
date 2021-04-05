namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientInhibitorAboutToBeReconstructedEvent : ILolClientInhibitorEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.InhibitorAboutToBeReconstructed;
    }
}