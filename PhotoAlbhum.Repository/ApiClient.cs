using Newtonsoft.Json;
using PhotoAlbum.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace PhotoAlbhum.Repository
{
    public class ApiClient : IApiClient
    {
        private readonly IConfig _config;
        private readonly HttpClient _httpClient;
        private const string Albums = "albums";
        private const string Photos = "photos";
        private const string Comments = "comments";
        public ApiClient(IConfig config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        public HttpStatusCode StatusCode { get; private set; }

        public string GetResponse(string requestUrl)
        {
            var content = string.Empty;

            if (_httpClient != null)
            {
                var httpContent = new HttpRequestMessage(HttpMethod.Get, requestUrl);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                                                       SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                var response = _httpClient.SendAsync(httpContent).Result;

                StatusCode = response.StatusCode;

                if (response.StatusCode != HttpStatusCode.OK &&
                    response.StatusCode != HttpStatusCode.NotFound)
                    throw new HttpRequestException($"Error request http status code {response.StatusCode}");

                content = response.Content.ReadAsStringAsync().Result;
            }

            return content;
        }

        public List<Album> GetAlbums()
        {
            var requestURL= string.Format(_config.ApiUrl + "/{0}",  Albums);
            return JsonConvert.DeserializeObject<List<Album>>(GetResponse(requestURL)); 
        }

        public List<Photo> GetPhotosByAlbumId(int id)
        {
            var requestURL = string.Format(_config.ApiUrl + "/{0}", Photos);
            var photos= JsonConvert.DeserializeObject<List<Photo>>(GetResponse(requestURL));
            return photos.Where(x => x.albumId == id).ToList();
        }

        public List<Comment> GetCommentsById(int postId)
        {
            var requestURL = string.Format(_config.ApiUrl + "/{0}", Comments);
            var photos = JsonConvert.DeserializeObject<List<Comment>>(GetResponse(requestURL));
            return photos.Where(x => x.postId == postId).ToList();
        }
    }
}
