using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.ImageReader
{
    internal sealed record RemoteImageData : IImageData
    {
        [JsonProperty]
        public ImageDataType Type => ImageDataType.Remote;
        
        [JsonProperty]
        public string DownloadUrl { get; init; } = string.Empty;

        [JsonProperty]
        public string RelativeFilePath { get; init; } = string.Empty;
    }
}