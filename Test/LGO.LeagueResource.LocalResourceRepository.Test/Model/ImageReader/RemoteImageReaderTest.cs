using System;
using System.IO;
using System.Threading.Tasks;
using LGO.Backend.Core.Model;
using LGO.LeagueApi.RemoteApiReader.Static;
using LGO.LeagueResource.LocalResourceRepository.Model.ImageReader;
using NUnit.Framework;

namespace LGO.LeagueResource.LocalResourceRepository.Test.Model.ImageReader
{
    [TestFixture]
    public class RemoteImageReaderTest
    {
        private static MultiComponentVersion GameVersion { get; } = new(11, 8, 1);
        
        [Test]
        public async Task TestImageIsCachedLocally()
        {
            var staticApiReader = new RemoteLeagueStaticApiReader();

            var localImageLocation = $"{Guid.NewGuid()}.jpg";
            var url = staticApiReader.GetItemSquareImageUrl(GameVersion, "1001"); // boots
            var downloads = 0;

            async Task<FileInfo?> DownloadAsync(string remoteUrl, FileInfo localFile)
            {
                downloads++;
                return await staticApiReader.DownloadFileAsync(remoteUrl, localFile);
            }

            var imageReader = RemoteImageReader.Of(new RemoteImageData
                                                   {
                                                       DownloadUrl = url,
                                                       RelativeFilePath = localImageLocation
                                                   }, DownloadAsync);

            if (File.Exists(localImageLocation))
            {
                File.Delete(localImageLocation);
            }

            var imageContent = await imageReader.ReadContentAsBase64Async();
            Assert.IsNotNull(imageContent);
            Assert.IsNotEmpty(imageContent);
            Assert.AreEqual(1, downloads);

            var imageContentWithoutExtraDownload = await imageReader.ReadContentAsBase64Async();
            Assert.IsNotNull(imageContentWithoutExtraDownload);
            Assert.IsNotEmpty(imageContentWithoutExtraDownload);
            Assert.AreEqual(1, downloads);
        }

        [Test]
        public async Task TestImageIsNotDownloadedIfItExists()
        {
            var staticApiReader = new RemoteLeagueStaticApiReader();

            var localImageLocation = $"{Guid.NewGuid()}.jpg";
            var url = staticApiReader.GetItemSquareImageUrl(GameVersion, "1001"); // boots
            await staticApiReader.DownloadFileAsync(url, new FileInfo(localImageLocation));
            
            Assert.IsTrue(File.Exists(localImageLocation));

            Task<FileInfo?> DownloadAsync(string remoteUrl, FileInfo localFile)
            {
                Assert.Fail($"{nameof(DownloadAsync)} must not be called.");
                return Task.FromResult<FileInfo?>(null);
            }

            var imageReader = RemoteImageReader.Of(new RemoteImageData
                                                   {
                                                       DownloadUrl = url,
                                                       RelativeFilePath = localImageLocation
                                                   }, DownloadAsync);
            
            var imageContent = await imageReader.ReadContentAsBase64Async();
            Assert.IsNotNull(imageContent);
            Assert.IsNotEmpty(imageContent);
        }
    }
}