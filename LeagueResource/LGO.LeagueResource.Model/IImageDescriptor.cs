using System;
using System.Threading.Tasks;

namespace LGO.LeagueResource.Model
{
    public interface IImageDescriptor
    {
        Guid Id { get; }
        
        Task<string?> ReadContentAsBase64Async();
    }
}