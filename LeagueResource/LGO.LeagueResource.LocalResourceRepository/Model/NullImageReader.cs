using System;
using System.Threading.Tasks;
using LGO.LeagueResource.Model;

namespace LGO.LeagueResource.LocalResourceRepository.Model
{
    internal sealed class NullImageReader : IImageReader
    {
        private static NullImageReader? _instance;

        public static NullImageReader Instance => _instance ??= new NullImageReader();

        public Guid Id => Guid.Empty;

        private NullImageReader()
        {
        }

        public Task<string?> ReadContentAsBase64Async()
        {
            return Task.FromResult<string?>(null);
        }
    }
}