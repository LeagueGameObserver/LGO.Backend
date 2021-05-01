using Newtonsoft.Json;

namespace LGO.LeagueResource.LocalResourceRepository.Model.ImageReader
{
    internal sealed record LocalImageData : IImageData
    {
        [JsonProperty]
        public ImageDataType Type => ImageDataType.Local;
        
        [JsonProperty]
        public string RelativeFilePath { get; init; } = string.Empty;
    }
}