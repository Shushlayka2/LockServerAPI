using LockServerAPI.Models.DataAccesses;
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
        }
    }
}
