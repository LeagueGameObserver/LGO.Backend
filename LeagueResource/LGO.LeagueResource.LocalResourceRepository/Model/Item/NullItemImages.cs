using System;
using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal sealed class NullItemImages : ILeagueResourceItemImages
    {
        private static Uri NullUri { get; } = new("http://null.null");
        
        private static NullItemImages? _instance;

        public static NullItemImages Instance => _instance ??= new NullItemImages();

        public Uri SquareImage => NullUri;

        private NullItemImages()
        {
        }
    }
}