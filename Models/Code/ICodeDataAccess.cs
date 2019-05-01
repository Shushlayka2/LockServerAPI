using System;

namespace LockServerAPI.Models.Code
{
    public interface ICodeDataAccess : IDisposable
    {
        string FindCode(string code);
    }
}
