using System;
using LGO.LeagueResource.Model.Champion;

namespace LGO.LeagueResource.LocalResourceRepository.Model.Champion
{
    internal class MutableLeagueResourceChampion : ILeagueResourceChampion
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public int Key { get; set; } = 0;

        public string Name { get; set; } = string.Empty;

        public ILeagueResourceChampionImages Images { get; set; } = NullChampionImages.Instance;
    }
}