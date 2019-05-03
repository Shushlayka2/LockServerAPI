using LockServerAPI.Models.BaseDataAccesses;

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
            var new_lock = new Lock()
            {
                Id = id
            };
            new_lock.GenerateDeviceId();
            Database.Locks.Add(new_lock);
            Database.SaveChanges();
            return new_lock.DeviceId;
        }
    }
}
