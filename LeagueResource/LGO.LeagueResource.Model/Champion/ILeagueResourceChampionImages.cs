namespace LGO.LeagueResource.Model.Champion
{
    public interface ILeagueResourceChampionImages
    {
        IImageDescriptor SplashImage { get; }
        
        IImageDescriptor LoadingImage { get; }
        
        IImageDescriptor SquareImage { get; }
    }
}