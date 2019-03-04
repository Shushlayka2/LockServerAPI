using System;

namespace LockServerAPI.Models.BaseDataAccesses
{
    public abstract class BaseDataAccess : IDisposable
    {
        public void Dispose()
        {
            //TODO Handle OnDispose behaviour
        }
    }
}
