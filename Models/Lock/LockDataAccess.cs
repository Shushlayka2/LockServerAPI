using LockServerAPI.Models.BaseDataAccesses;
using Microsoft.EntityFrameworkCore;

namespace LockServerAPI.Models.Lock
{
    public class LockDataAccess : BaseDataAccess, ILockDataAccess
    {
        /// <summary>
        /// Ctr
        /// </summary>
        /// <param name="database"></param>
        /// <param name="database">Database context</param>
        public LockDataAccess(DatabaseContext database)
            : base(database)
        {
        }

        /// <summary>
        /// Creates new record for Lock table
        /// </summary>
        /// <param name="id">Lock identifier</param>
        /// <param name="config">Connection configuration</param>
        /// <returns></returns>
        public string RegisterLock(string lockId, string config)
        {
            var newLock = new Lock()
            {
                LockId = lockId,
                Config = config
            };
            newLock.GenerateDeviceId();
            Database.Locks.Add(newLock);
            Database.SaveChanges();
            return newLock.DeviceId;
        }
    }
}
