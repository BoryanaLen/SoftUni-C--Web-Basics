using IRunes.App.ViewModels.Albums;
using IRunes.App.ViewModels.Tracks;
using IRunes.Services;
using SIS.HTTP;
using SIS.MvcFramework;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;
        private readonly ITracksService tracksService;

        public AlbumsController(IAlbumsService albumsService, ITracksService tracksService)
        {
            this.albumsService = albumsService;
            this.tracksService = tracksService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var albums = this.albumsService.GetAllAlbums()
                .Select(x => new AlbumListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            return this.View(new AlbumsAllViewModel { Albums = albums}, "All");
        }

        public HttpResponse Create()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(AlbumCreateInputModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Password must be at least 4 characters and at most 20");
            }

            if (string.IsNullOrWhiteSpace(model.Cover))
            {
                return this.Error("Cover cannot be empty!");
            }

            this.albumsService.CreateAlbum(model.Name, model.Cover);

            return this.Redirect("/Albums/All");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var album = this.albumsService.GetAlbumById(id);

            var albumTracks = this.tracksService.GetAllTracksInAlbum(id);

            var tracks = albumTracks
                .Select(x => new TrackListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            var viewModel = new AlbumDetailsViewModel
            {
                Id = album.Id,
                Name = album.Name,
                Cover = album.Cover,
                Price = albumTracks.Sum(x => x.Price) * 0.87M,
                Tracks = tracks,
            };

            return this.View(viewModel);
        }
    }
}
