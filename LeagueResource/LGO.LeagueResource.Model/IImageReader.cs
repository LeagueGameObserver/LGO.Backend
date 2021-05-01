using System;
using System.Threading.Tasks;

namespace LGO.LeagueResource.Model
{
    public interface IImageReader
    {
        Task<string?> ReadContentAsBase64Async();
    }
}