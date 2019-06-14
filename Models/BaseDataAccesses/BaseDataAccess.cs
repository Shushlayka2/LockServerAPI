using System;

namespace LockServerAPI.Models.BaseDataAccesses
{
    public abstract class BaseDataAccess : IDisposable
    {
        public DatabaseContext Database { get; }

        public BaseDataAccess(DatabaseContext database)
        {
            Database = database;
        }

        public void Dispose()
        {
        }
    }
}
