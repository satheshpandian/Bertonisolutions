using PhotoAlbum.Models;
using System.Collections.Generic;

namespace PhotoAlbhum.Repository
{
    public interface IApiClient
    {
        List<Album> GetAlbums();
        List<Photo> GetPhotosByAlbumId(int id);
        List<Comment> GetCommentsById(int postId);
    }
}