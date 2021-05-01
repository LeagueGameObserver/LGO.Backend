namespace LGO.LeagueResource.Model.Champion
{
    public interface ILeagueResourceChampionImages
    {
        IImageReader SplashImage { get; }
        
        IImageReader LoadingImage { get; }
        
        IImageReader SquareImage { get; }
    }
}