using System;

namespace LockServerAPI.Models.User
{
    public interface IUserDataAccess : IDisposable
    {
        bool FindUser(string username, string password);
    }
}
