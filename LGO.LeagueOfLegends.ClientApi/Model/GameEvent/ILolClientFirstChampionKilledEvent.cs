namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientFirstChampionKilledEvent : ILolClientGameEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.FirstChampionKilled;
        
        string KillerSummonerName { get; }
    }
}