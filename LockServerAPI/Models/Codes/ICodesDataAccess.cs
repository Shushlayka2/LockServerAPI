using System;

namespace LockServerAPI.Models.DataAccesses
{
    public interface ICodesDataAccess : IDisposable
    {
        string FindCode(string code);
    }
}
