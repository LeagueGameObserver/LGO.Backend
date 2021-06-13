using System;
using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal class MutableLeagueResourceItemImages : ILeagueResourceItemImages
    {
        public Uri SquareImage { get; set; } = null!;
    }
}