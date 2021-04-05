namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientMultipleChampionsKilledEvent : ILolClientKillerEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.MultipleChampionsKilled;
        
        int NumberOfKills { get; }
    }
}