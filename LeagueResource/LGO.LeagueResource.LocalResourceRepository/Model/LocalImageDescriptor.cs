using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;
using LGO.LeagueResource.Model;
using log4net;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace LGO.LeagueResource.LocalResourceRepository.Model
{
    internal sealed class LocalImageDescriptor : IImageDescriptor
    {
        private static ILog Log { get; } = LogManager.GetLogger(typeof(LocalImageDescriptor));
        private static ConcurrentDictionary<string, LocalImageDescriptor> Instances { get; } = new();

        public static LocalImageDescriptor ForFile(FileInfo file)
        {
            if (!System.IO.File.Exists(file.FullName))
            {
                throw new FileNotFoundException($"\"{file.FullName}\" cannot be found.");
            }

            return Instances.GetOrAdd(file.FullName, _ =>
                                                     {
                                                         Log.Debug($"Creating new {nameof(LocalImageDescriptor)} instance for \"{file.FullName}\". {Instances.Count + 1} instances in total.");
                                                         return new LocalImageDescriptor(file);
                                                     });
        }

        public Guid Id { get; } = Guid.NewGuid();

        private FileInfo File { get; }
        
        private LocalImageDescriptor(FileInfo file)
        {
            File = file;
        }
        
        public async Task<string?> ReadContentAsBase64Async()
        {
            using var image = await Image.LoadAsync(Configuration.Default, File.FullName);
            await using var memoryStream = new MemoryStream();
            await image.SaveAsync(memoryStream, PngFormat.Instance);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}