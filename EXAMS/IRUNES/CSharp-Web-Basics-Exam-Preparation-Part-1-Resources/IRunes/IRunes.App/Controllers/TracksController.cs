using IRunes.App.ViewModels.Tracks;
using IRunes.Services;
using SIS.HTTP;
using SIS.MvcFramework;
using System.Linq;

namespace IRunes.App.Controllers
{
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;
        private readonly IAlbumsService albumsService;

        public TracksController(ITracksService tracksService, IAlbumsService albumsService)
        {
            this.tracksService = tracksService;
            this.albumsService = albumsService;
        }

        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var album = this.albumsService.GetAlbumById(albumId);

            if (album == null)
            {
                return this.Error("Album not found!");
            }

            return this.View(new TrackCreateViewModel { AlbumId = albumId});
        }

        [HttpPost]
        public HttpResponse Create(TrackCreateInputModel model)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (model.Name.Length < 4 || model.Name.Length > 20)
            {
                return this.Error("Password must be at least 4 characters and at most 20");
            }

            if (string.IsNullOrWhiteSpace(model.Link))
            {
                return this.Error("Cover cannot be empty!");
            }

            if (model.Price < 0)
            {
                return this.Error("Price must be more than zero");
            }

            this.tracksService.CreateTrack(model.Name, model.Link, model.Price, model.AlbumId);

            return this.Redirect($"/Albums/Details?id={model.AlbumId}");
        }


        public HttpResponse Details(string albumId, string trackId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var track = this.tracksService.GetTrackById(trackId);

            var viewModel = new TrackDetailsViewModel
            {
                Id = track.Id,
                Name = track.Name,
                Price = track.Price,
                Link = track.Link,
                AlbumId = albumId,
            };

            return this.View(viewModel);
        }
    }
}
