namespace LockServerAPI.Models.BaseDataAccesses
{
    public interface IDataAccessService
    {
        T GetDataAccess<T>();
    }
}
