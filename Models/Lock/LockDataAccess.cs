using LockServerAPI.Models.BaseDataAccesses;
using System;

namespace LockServerAPI.Models.Lock
{
    public class LockDataAccess : BaseDataAccess, ILockDataAccess
    {
        public LockDataAccess(DatabaseContext database)
            : base(database)
        {
        }

        public string RegisterLock(string id)
        {
            var deviceId = Guid.NewGuid().ToString();
            Database.Locks.Add(new Lock()
            {
                Id = id,
                DeviceId = deviceId
            });
            Database.SaveChanges();
            return deviceId;
        }
    }
}
