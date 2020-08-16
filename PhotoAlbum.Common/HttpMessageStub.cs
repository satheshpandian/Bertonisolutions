using Moq;
using Moq.Protected;
using System.Collections;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PhotoAlbum.Common
{
    public class HttpMessageStub
    {
        private const string ConstHttpStatus400 = "HttpStatus400";
        private const string ConstHttpStatus404 = "HttpStatus404";
        private const string ConstHttpStatus200 = "HttpStatus200";
        private const string ConstAlbums = "albums";
        private const string ConstPhotos = "photos";
        private const string ConstComments = "comments";

        public HttpMessageStub(string requestUrl)
        {
            RequestURL = requestUrl;
        }

        public string RequestURL { get; set; }

        public Mock<HttpMessageHandler> CreateMockHttpMessageStub(JsonCollectionType jsonCollectionType)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Default);
            handlerMock
                .Protected()
                // Setup the PROTECTED method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(GetStringContent( jsonCollectionType))
                .Verifiable();
            return handlerMock;
        }

        public HttpResponseMessage GetStringContent(JsonCollectionType jsonCollectionType)
        {
            var response = new HttpResponseMessage();
            if (RequestURL != null)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Content = new StringContent(GetJsonCollectionTypeResponse(jsonCollectionType));
            }
            else if (RequestURL != null && RequestURL.IndexOf(ConstHttpStatus400) >= 0)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            else if (RequestURL != null && RequestURL.IndexOf(ConstHttpStatus404) >= 0)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Content = new StringContent(
                    "{ \"timestampUtc\": \"2018-08-26T06:16:38.3973386Z\", " +
                    " \"exceptionType\": \"EntityNotFoundException\", " +
                    " \"httpStatusCode\": 404, " +
                    " \"httpStatus\": \"NotFound\" } ");
            }

            return response;
        }

        private string GetJsonCollectionTypeResponse(JsonCollectionType jsonCollectionType)
        {
            string resp = string.Empty;
            switch (jsonCollectionType)
            {
                case JsonCollectionType.Album:
                    resp=                            "[{" +
                                                     "  \"userId\": \"1\"," +
                                                     "  \"id\": \"1\"," +
                                                     "  \"title\": \"quidem molestiae enim\"" +
                                                     "}]";
                    break;
                case JsonCollectionType.Photo:
                    resp = "[{" +
                                                     "  \"albumId\": \"1\"," +
                                                     "  \"id\": \"1\"," +
                                                     "  \"title\": \"accusamus beatae ad facilis cum similique qui sunt\"," +
                                                     "  \"url\": \"https://via.placeholder.com/600/92c952\"" +
                                                     "}]";
                    break;
                case JsonCollectionType.Comment:
                    resp = "[{" +
                                                     "  \"postId\": \"1\"," +
                                                     "  \"id\": \"1\"," +
                                                      "  \"email\": \"Eliseo@gardner.biz\"," +
                                                     "  \"name\": \"quidem molestiae enim\"" +
                                                     "}]";
                    break;
            }
            return resp;
        }
    }

    public enum JsonCollectionType
    {
        Album,
        Photo,
        Comment
    }
}
