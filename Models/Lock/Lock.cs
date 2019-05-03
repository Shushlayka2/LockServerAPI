using System;

namespace LockServerAPI.Models.Lock
{
    public partial class Lock
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }

        public void GenerateDeviceId()
        {
            DeviceId = Guid.NewGuid().ToString();
        }
    }
}
