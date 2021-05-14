using LGO.Backend.Model.League.Enum;

namespace LGO.Backend.Model.League.GameEvent
{
    internal sealed class MutableLeagueChampionKilledEvent : MutableLeagueKillerWithAssistersEvent, ILeagueChampionKilledEvent
    {
        public override LeagueGameEventType Type => LeagueGameEventType.ChampionKilled;
        public string VictimSummonerName { get; set; } = string.Empty;
    }
}