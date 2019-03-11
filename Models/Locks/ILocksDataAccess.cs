using System;

namespace LockServerAPI.Models.Locks
{
    public interface ILocksDataAccess : IDisposable
    {
        string RegisterLock(string id);
    }
}
