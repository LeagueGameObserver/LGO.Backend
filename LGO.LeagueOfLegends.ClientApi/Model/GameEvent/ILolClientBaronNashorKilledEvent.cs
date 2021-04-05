namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientBaronNashorKilledEvent : ILolClientNeutralObjectiveKilledEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.BaronNashorKilled;
    }
}