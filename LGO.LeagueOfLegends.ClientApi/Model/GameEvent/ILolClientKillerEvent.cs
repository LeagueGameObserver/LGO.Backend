namespace LGO.LeagueOfLegends.ClientApi.Model.GameEvent
{
    public interface ILolClientKillerEvent : ILolClientGameEvent
    {
        string KillerName { get; }
    }
}