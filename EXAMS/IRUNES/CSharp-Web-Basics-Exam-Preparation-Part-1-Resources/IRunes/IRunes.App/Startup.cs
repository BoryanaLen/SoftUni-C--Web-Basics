using IRunes.Data;
using IRunes.Services;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP;
using SIS.MvcFramework;
using System.Collections.Generic;

namespace IRunes.App
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IAlbumsService, AlbumsService>();
            serviceCollection.Add<ITracksService, TracksServices>();
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new IRunesDbContext();
            db.Database.Migrate();
        }
    }
}
