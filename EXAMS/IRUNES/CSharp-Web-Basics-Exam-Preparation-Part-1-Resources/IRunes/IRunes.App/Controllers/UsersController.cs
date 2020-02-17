using IRunes.App.ViewModels.Users;
using IRunes.Services;
using SIS.HTTP;
using SIS.MvcFramework;
using System;
using System.Net.Mail;

namespace IRunes.App.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterInputModel model)
        {
            if (model.Username?.Length < 4 || model.Username?.Length > 10)
            {
                return this.Error("Username should be between 4 and 10 characters .");
            }

            if (this.usersService.IsUsernameUsed(model.Username))
            {
                return this.Error("Username already used!");
            }

            if (model.Password?.Length < 6 || model.Password?.Length > 20)
            {
                return this.Error("Password should be between 6 and 20 characters.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.Error("Passwords should be the same!");
            }

            if (!IsValid(model.Email))
            {
                return this.Error("Invalid email!");
            }

            if (this.usersService.IsEmailUsed(model.Email))
            {
                return this.Error("Email already used!");
            }

            this.usersService.CreateUser(model.Username, model.Email, model.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this.usersService.GetUserId(username, password);

            if (userId == null)
            {
                return this.Redirect("/Users/Login");
            }

            this.SignIn(userId);

            return this.Redirect("/");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.SignOut();

            return this.Redirect("/");
        }

        private bool IsValid(string emailaddress)
        {
            try
            {
                new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
