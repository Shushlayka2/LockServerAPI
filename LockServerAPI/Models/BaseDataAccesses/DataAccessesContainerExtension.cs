using LockServerAPI.Models.Codes;
using LockServerAPI.Models.Locks;
using Unity;
using Unity.Extension;

namespace LockServerAPI.Models.BaseDataAccesses
{
    public class DataAccessesContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IDataAccessService, DataAccessService>();
            Container.RegisterType<ICodesDataAccess, CodesDataAccess>();
            Container.RegisterType<ILocksDataAccess, LocksDataAccess>();
        }
    }
}
