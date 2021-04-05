namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientChampionKilledEvent : ILolClientKillerEvent, ILolClientAssistersEvent
    {
        LolClientGameEventType ILolClientGameEvent.Type => LolClientGameEventType.ChampionKilled;
        
        string VictimSummonerName { get; }
    }
}