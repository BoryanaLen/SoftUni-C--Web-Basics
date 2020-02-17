using Microsoft.EntityFrameworkCore;
using SIS.HTTP;
using SIS.MvcFramework;
using Suls.Data;
using Suls.Services;
using Suls.Services.Contracts;
using System.Collections.Generic;

namespace Suls.Web
{
    public class Startup : IMvcApplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<IProblemsService, ProblemsService>();
            serviceCollection.Add<ISubmissionsService, SubmissionsService>();
        }

        public void Configure(IList<Route> routeTable)
        {
            var db = new SulsDbContext();
            db.Database.Migrate();
        }
    }
}
