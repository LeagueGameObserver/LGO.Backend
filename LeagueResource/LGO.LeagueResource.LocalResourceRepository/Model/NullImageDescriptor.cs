using System;
using System.Threading.Tasks;
using LGO.LeagueResource.Model;

namespace LGO.LeagueResource.LocalResourceRepository.Model
{
    internal sealed class NullImageDescriptor : IImageDescriptor
    {
        private static NullImageDescriptor? _instance;

        public static NullImageDescriptor Instance => _instance ??= new NullImageDescriptor();

        public Guid Id => Guid.Empty;

        private NullImageDescriptor()
        {
        }

        public Task<string?> ReadContentAsBase64Async()
        {
            return Task.FromResult<string?>(null);
        }
    }
}