using IRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public interface IAlbumsService
    {
        IQueryable<Album> GetAllAlbums();

        void CreateAlbum(string name, string cover);

        Album GetAlbumById(string id);
    }
}
