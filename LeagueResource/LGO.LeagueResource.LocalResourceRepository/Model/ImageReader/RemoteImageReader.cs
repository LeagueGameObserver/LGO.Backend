using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using LGO.LeagueResource.Model;
using log4net;

namespace LGO.LeagueResource.LocalResourceRepository.Model.ImageReader
{
    internal sealed class RemoteImageReader : IImageReader
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(RemoteImageReader));
        private static ConcurrentDictionary<RemoteImageData, RemoteImageReader> Instances { get; } = new();

        public static RemoteImageReader Of(RemoteImageData remoteImageData, Func<string, FileInfo, Task<FileInfo?>> downloadAsync)
        {
            return Instances.GetOrAdd(remoteImageData, _ =>
                                           {
                                               Log.Debug($"Creating new {nameof(RemoteImageReader)} instance for \"{remoteImageData.DownloadUrl}\". {Instances.Count + 1} instances in total.");
                                               return new RemoteImageReader(remoteImageData, downloadAsync);
                                           });
        }

        public RemoteImageData ImageData { get; }
        
        private Func<string, FileInfo, Task<FileInfo?>> DownloadAsync { get; }
        private LocalImageReader? _localImage;

        private RemoteImageReader(RemoteImageData imageData, Func<string, FileInfo, Task<FileInfo?>> downloadAsync)
        {
            ImageData = imageData;
            DownloadAsync = downloadAsync;
        }

        public async Task<string?> ReadContentAsBase64Async()
        {
            if (_localImage != null)
            {
                return await _localImage.ReadContentAsBase64Async();
            }

            if (File.Exists(ImageData.RelativeFilePath))
            {
                _localImage = LocalImageReader.Of(new LocalImageData {RelativeFilePath = ImageData.RelativeFilePath});
                return await _localImage.ReadContentAsBase64Async();
            }

            var fileInfo = await DownloadAsync.Invoke(ImageData.DownloadUrl, new FileInfo(ImageData.RelativeFilePath));
            if (fileInfo == null)
            {
                return null;
            }
            
            _localImage = LocalImageReader.Of(new LocalImageData {RelativeFilePath = ImageData.RelativeFilePath});
            return await _localImage.ReadContentAsBase64Async();
        }
    }
}