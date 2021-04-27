using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using LGO.LeagueResource.Model;
using log4net;

namespace LGO.LeagueResource.LocalResourceRepository.Model
{
    internal sealed class RemoteImageDescriptor : IImageDescriptor
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(RemoteImageDescriptor));
        private static ConcurrentDictionary<string, RemoteImageDescriptor> Instances { get; } = new();

        public static RemoteImageDescriptor ForUrl(string url, Func<string, Task<FileInfo?>> download)
        {
            return Instances.GetOrAdd(url, _ =>
                                           {
                                               Log.Debug($"Creating new {nameof(RemoteImageDescriptor)} instance for \"{url}\". {Instances.Count + 1} instances in total.");
                                               return new RemoteImageDescriptor(url, download);
                                           });
        }

        public Guid Id { get; } = Guid.NewGuid();

        private string Url { get; }
        private Func<string, Task<FileInfo?>> Download { get; }
        private LocalImageDescriptor? _localImage;

        private RemoteImageDescriptor(string url, Func<string, Task<FileInfo?>> download)
        {
            Url = url;
            Download = download;
        }

        public async Task<string?> ReadContentAsBase64Async()
        {
            if (_localImage != null)
            {
                return await _localImage.ReadContentAsBase64Async();
            }

            var fileInfo = await Download.Invoke(Url);
            if (fileInfo == null)
            {
                return null;
            }
            
            _localImage = LocalImageDescriptor.ForFile(fileInfo);
            return await _localImage.ReadContentAsBase64Async();
        }
    }
}