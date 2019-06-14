using Microsoft.Azure.Devices;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace LockServerAPI.Services
{
    public class IoTServiceProxy : IIoTServiceProxy
    {
        private ServiceClient serviceClient;

        private string lockId;

        public IoTServiceProxy(string lockId, string config)
        {
            this.lockId = lockId;
            serviceClient = ServiceClient.CreateFromConnectionString(config);
        }

        public async Task<bool> RegisterDevice(string deviceId)
        {
            var methodInvocation = new CloudToDeviceMethod("RegisterDevice") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            methodInvocation.SetPayloadJson(String.Format("{\"deviceId\": \"{0}\"}", deviceId));
            var response = await serviceClient.InvokeDeviceMethodAsync(lockId, methodInvocation);
            var isRegistered = bool.Parse(JObject.Parse(response.GetPayloadAsJson())["result"].ToString());
            return isRegistered;
        }
    }
}
