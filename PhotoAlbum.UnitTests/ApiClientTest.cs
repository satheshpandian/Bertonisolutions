using Moq;
using NUnit.Framework;
using PhotoAlbhum.Repository;
using PhotoAlbum.Common;
using System.Linq;
using System.Net.Http;

namespace PhotoAlbum.UnitTests
{
    public class ApiClientTest
    {
        private readonly Mock<IConfig> ConfigMock;
        private IApiClient ApiClient;
        private string RoadID;

        public ApiClientTest()
        {
            ConfigMock = new Mock<IConfig>();
        }

        [Test]
        public void When_Execute_Request_With_Correct_Url_For_Albums()
        {
            SetupConfigUri("http://testurl");
            ApiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub(JsonCollectionType.Album).Object));
            var albums =ApiClient.GetAlbums();
            Assert.NotNull(albums);
            Assert.AreEqual(albums.Count,1);
        }
        [Test]
        public void When_Execute_Request_With_Correct_Url_For_Photos()
        {
            SetupConfigUri("http://testurl");
            ApiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub(JsonCollectionType.Photo).Object));
            var photos = ApiClient.GetPhotosByAlbumId(1);
            Assert.NotNull(photos);
            Assert.AreEqual(photos.Count, 1);
        }
        [Test]
        public void When_Execute_Request_With_Correct_Url_For_Comments()
        {
            SetupConfigUri("http://testurl");
            ApiClient = new ApiClient(ConfigMock.Object, new HttpClient(new HttpMessageStub(ConfigMock.Object.ApiUrl).CreateMockHttpMessageStub(JsonCollectionType.Comment).Object));
            var comments = ApiClient.GetCommentsById(1);
            Assert.NotNull(comments);
            Assert.AreEqual(comments.Count, 1);
        }

        private void SetupConfigUri(string uri)
        {
            ConfigMock.Setup(c => c.ApiUrl).Returns(() => uri);
        }
    }
}
