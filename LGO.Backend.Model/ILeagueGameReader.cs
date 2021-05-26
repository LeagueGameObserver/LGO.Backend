using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LGO.Backend.Model.League;
using LGO.Backend.Model.League.MatchUp;
using LGO.Backend.Model.League.MatchUp.Descriptor;

namespace LGO.Backend.Model
{
    public interface ILeagueGameReader
    {
        Guid GameId { get; }
        
        IEnumerable<ILeagueMatchUpDescriptor> MatchUpDescriptors { get; }

        bool TryRemoveMatchUpDescriptor(Guid matchUpId);

        bool TryAddMatchUpDescriptor(ILeagueMatchUpDescriptor matchUpDescriptor);

        bool AddOrReplaceMatchUpDescriptor(ILeagueMatchUpDescriptor matchUpDescriptor);

        Task<ILeagueGame> ReadGameAsync(ILgoClientSettings clientSettings);
    }
}