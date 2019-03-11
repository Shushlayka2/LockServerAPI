using System;

namespace LockServerAPI.Models.Codes
{
    public interface ICodesDataAccess : IDisposable
    {
        string FindCode(string code);
    }
}
