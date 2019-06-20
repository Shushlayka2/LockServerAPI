using System;

namespace LockServerAPI.Models.Lock
{
    public partial class Lock
    {
        public int Id { get; set; }
        public string LockId { get; set; }
        public string DeviceId { get; set; }
        public string Config { get; set; }

        public void GenerateDeviceId()
        {
            DeviceId = Guid.NewGuid().ToString();
        }
    }
}
