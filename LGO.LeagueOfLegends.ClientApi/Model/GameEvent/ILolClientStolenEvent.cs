namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientStolenEvent : ILolClientGameEvent
    {
        bool HasBeenStolen { get; }
    }
}