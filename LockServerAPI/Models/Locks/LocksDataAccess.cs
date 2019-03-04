using LockServerAPI.Models.BaseDataAccesses;
using System;

namespace LockServerAPI.Models.Locks
{
    public class LocksDataAccess : BaseDataAccess, ILocksDataAccess
    {
        public LocksDataAccess(DatabaseContext database)
            : base(database)
        {
        }

        public string RegisterLock(string id)
        {
            var deviceId = Guid.NewGuid().ToString();
            Database.Locks.Add(new Locks()
            {
                Id = id,
                DeviceId = deviceId
            });
            Database.SaveChanges();
            return deviceId;
        }
    }
}
