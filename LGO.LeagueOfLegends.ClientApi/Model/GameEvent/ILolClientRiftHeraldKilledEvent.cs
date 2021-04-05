namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientRiftHeraldKilledEvent : ILolClientNeutralObjectiveKilledEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.RiftHeraldKilled;
    }
}