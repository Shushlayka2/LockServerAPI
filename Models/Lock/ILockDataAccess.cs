using System;

namespace LockServerAPI.Models.Lock
{
    public interface ILockDataAccess : IDisposable
    {
        string RegisterLock(string id);
    }
}
