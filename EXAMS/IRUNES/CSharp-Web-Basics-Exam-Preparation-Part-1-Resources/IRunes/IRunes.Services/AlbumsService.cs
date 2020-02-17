using IRunes.Data;
using IRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly IRunesDbContext db;

        public AlbumsService(IRunesDbContext db)
        {
            this.db = db;
        }

        public void CreateAlbum(string name, string cover)
        {
            var album = new Album
            {
                Name = name,
                Cover = cover,
            };

            this.db.Albums.Add(album);

            this.db.SaveChanges();
        }

        public Album GetAlbumById(string id)
        {
            return this.db.Albums.Where(x => x.Id == id).FirstOrDefault();
        }

        public IQueryable<Album> GetAllAlbums()
        {
            return this.db.Albums.AsQueryable();
        }
    }
}
