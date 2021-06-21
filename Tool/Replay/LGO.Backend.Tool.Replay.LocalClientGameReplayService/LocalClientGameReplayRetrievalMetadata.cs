using System;
using System.IO;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.Backend.Core.Model.League.Enum;
using LGO.Backend.Model;
using Newtonsoft.Json;

namespace LGO.Backend.Tool.Replay.LocalClientGameReplayService
{
    public sealed class LocalClientGameReplayRetrievalMetadata : ILeagueClientGameRetrievalMetadata
    {
        public const string MetadataFileName = "$metadata.json";
        
        public TimeSpan? PollingInterval { get; }
        public MultiComponentVersion GameVersion { get; }
        public LeagueLocalizationType GameLocalization { get; }
        
        [JsonIgnore]
        public DirectoryInfo SnapshotDirectory { get; private set; }

        public static async Task<LocalClientGameReplayRetrievalMetadata> ReadFromSnapshotDirectory(DirectoryInfo snapshotDirectory)
        {
            return await ReadFromFileAsync(new FileInfo(Path.Combine(snapshotDirectory.FullName, MetadataFileName)));
        }

        public static async Task<LocalClientGameReplayRetrievalMetadata> ReadFromFileAsync(FileInfo serializedMetadata)
        {
            if (!File.Exists(serializedMetadata.FullName))
            {
                throw new ArgumentException($"\"{serializedMetadata.FullName}\" does not exist.");
            }
            
            var fileContent = await File.ReadAllTextAsync(serializedMetadata.FullName);
            var metadata = JsonConvert.DeserializeObject<LocalClientGameReplayRetrievalMetadata>(fileContent);
            if (metadata == null)
            {
                throw new ArgumentException($"Unable to deserialize content from \"{serializedMetadata.FullName}\" to an {nameof(LocalClientGameReplayRetrievalMetadata)} instance.");
            }

            if (string.IsNullOrEmpty(serializedMetadata.DirectoryName))
            {
                throw new ArgumentException($"Unable to determine the {nameof(SnapshotDirectory)} from the given {nameof(FileInfo)} (\"{serializedMetadata.FullName}\").");
            }
            
            metadata.SnapshotDirectory = new DirectoryInfo(serializedMetadata.DirectoryName);
            return metadata;
        }

        public LocalClientGameReplayRetrievalMetadata(TimeSpan pollingInterval, MultiComponentVersion gameVersion, LeagueLocalizationType localization, DirectoryInfo snapshotDirectory)
        {
            PollingInterval = pollingInterval;
            GameVersion = gameVersion;
            GameLocalization = localization;
            SnapshotDirectory = snapshotDirectory;
        }

        public async Task WriteToFile(FileInfo file)
        {
            var fileContent = JsonConvert.SerializeObject(this);
            await File.WriteAllTextAsync(file.FullName, fileContent);
        }
    }
}