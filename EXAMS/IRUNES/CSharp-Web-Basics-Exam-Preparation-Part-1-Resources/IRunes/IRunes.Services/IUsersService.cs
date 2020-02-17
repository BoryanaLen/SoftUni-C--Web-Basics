using IRunes.Models;
using System;

namespace IRunes.Services
{
    public interface IUsersService
    {
        User GetUserById(string id);

        void CreateUser(string username, string email, string password);

        string GetUserId(string username, string password);

        bool IsUsernameUsed(string username);

        bool IsEmailUsed(string email);
    }
}
