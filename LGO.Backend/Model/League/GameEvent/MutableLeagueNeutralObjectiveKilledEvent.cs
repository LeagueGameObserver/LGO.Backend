namespace LGO.Backend.Model.League.GameEvent
{
    internal abstract class MutableLeagueNeutralObjectiveKilledEvent : MutableLeagueKillerWithAssistersEvent, ILeagueNeutralObjectiveKilledEvent
    {
        public bool WasStolen { get; set; } = false;
    }
}