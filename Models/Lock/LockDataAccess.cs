using LockServerAPI.Models.BaseDataAccesses;

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
        public string RegisterLock(string id, string config)
        {
            var newLock = new Lock()
            {
                Id = id,
                Config = config
            };
            newLock.GenerateDeviceId();
            Database.Locks.Add(newLock);
            Database.SaveChanges();
            return newLock.DeviceId;
        }
    }
}
