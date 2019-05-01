using LockServerAPI.Models.Code;
using LockServerAPI.Models.Lock;
using LockServerAPI.Models.User;
using Unity;
using Unity.Extension;

namespace LockServerAPI.Models.BaseDataAccesses
{
    public class DataAccessesContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IDataAccessService, DataAccessService>();
            Container.RegisterType<ICodeDataAccess, CodeDataAccess>();
            Container.RegisterType<ILockDataAccess, LockDataAccess>();
            Container.RegisterType<IUserDataAccess, UserDataAccess>();
        }
    }
}
