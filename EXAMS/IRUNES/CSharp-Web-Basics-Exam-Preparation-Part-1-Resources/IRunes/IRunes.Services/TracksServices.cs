using IRunes.Data;
using IRunes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IRunes.Services
{
    public class TracksServices : ITracksService
    {
        private readonly IRunesDbContext db;
        private readonly IAlbumsService albumsService;

        public TracksServices(IRunesDbContext db, IAlbumsService albumsService)
        {
            this.db = db;
            this.albumsService = albumsService;
        }

        public void CreateTrack(string name, string link, decimal price, string albumId)
        {
            var track = new Track
            {
                Name = name,
                Link = link,
                Price = price,
                AlbumId = albumId,
            };

            this.db.Tracks.Add(track);

            var album = this.albumsService.GetAlbumById(albumId);

            album.Price = this.GetAllTracksInAlbum(albumId).Select(x => x.Price).Sum() * 0.87M;

            this.db.SaveChanges();
        }

        public IQueryable<Track> GetAllTracksInAlbum(string albumId)
        {
            return this.db.Tracks.Where(x => x.AlbumId == albumId).AsQueryable();
        }

        public Track GetTrackById(string id)
        {
            return this.db.Tracks.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
