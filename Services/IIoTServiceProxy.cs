using System.Threading.Tasks;

namespace LockServerAPI.Services
{
    public interface IIoTServiceProxy
    {
        Task<bool> RegisterDevice(string deviceId);
    }
}
