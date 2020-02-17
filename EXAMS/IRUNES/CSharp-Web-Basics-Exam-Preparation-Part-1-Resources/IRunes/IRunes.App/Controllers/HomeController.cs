using IRunes.App.ViewModels.Users;
using IRunes.Services;
using SIS.HTTP;
using SIS.MvcFramework;

namespace IRunes.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;

        public HomeController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                string usertId = this.User;

                var user = this.usersService.GetUserById(usertId);

                return this.View(new LoggedUserViewModel { Username = user.Username }, "Home");
            }
            else
            {
                return this.View();
            }
        }
    }
}
