using Unity;

namespace LockServerAPI.Models.BaseDataAccesses
{
    public class DataAccessService : IDataAccessService
    {
        static IUnityContainer Container;

        public DataAccessService(IUnityContainer container)
        {
            Container = container;
        }

        public T GetDataAccess<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
