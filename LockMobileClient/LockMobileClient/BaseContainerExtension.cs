using LockMobileClient.Services;
using LockMobileClient.ViewModels;
using Unity;
using Unity.Extension;

namespace LockMobileClient
{
    public class BaseContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IRemoteServerSyncProxy, RemoteServerSyncProxy>();
            Container.RegisterType<RegistrationViewModel>();
        }
    }
}
