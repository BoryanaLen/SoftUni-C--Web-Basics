using IRunes.Models;
using System.Linq;

namespace IRunes.Services
{
    public interface ITracksService
    {
        IQueryable<Track> GetAllTracksInAlbum(string albumId);

        void CreateTrack(string name, string link, decimal price, string albumId);

        Track GetTrackById(string id);
    }
}
