using LGO.LeagueResource.Model;
using LGO.LeagueResource.Model.Item;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Item
{
    internal class MutableLeagueResourceItemImages : ILeagueResourceItemImages
    {
        public IImageDescriptor SquareImage { get; set; } = NullImageDescriptor.Instance;
    }
}