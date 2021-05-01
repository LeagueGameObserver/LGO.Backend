using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using LGO.LeagueResource.Model;
using log4net;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace LGO.LeagueResource.LocalResourceRepository.Model.ImageReader
{
    internal sealed class LocalImageReader : IImageReader
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LocalImageReader));
        
        private static ConcurrentDictionary<LocalImageData, LocalImageReader> Instances { get; } = new();

        public static LocalImageReader Of(LocalImageData localImageData)
        {
            if (!File.Exists(localImageData.RelativeFilePath))
            {
                throw new FileNotFoundException($"\"{localImageData.RelativeFilePath}\" cannot be found.");
            }

            return Instances.GetOrAdd(localImageData, _ =>
                                                      {
                                                          Log.Debug($"Creating new {nameof(LocalImageReader)} instance for \"{localImageData.RelativeFilePath}\". {Instances.Count + 1} instances in total.");
                                                          return new LocalImageReader(localImageData);
                                                      });
        }
        
        public LocalImageData ImageData { get; }
        
        private LocalImageReader(LocalImageData imageData)
        {
            ImageData = imageData;
        }

        public async Task<string?> ReadContentAsBase64Async()
        {
            using var image = await Image.LoadAsync(Configuration.Default, ImageData.RelativeFilePath);
            await using var memoryStream = new MemoryStream();
            await image.SaveAsync(memoryStream, PngFormat.Instance);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}