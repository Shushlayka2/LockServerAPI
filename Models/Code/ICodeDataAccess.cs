using System;
using System.Collections.Generic;

namespace LockServerAPI.Models.Code
{
    public interface ICodeDataAccess : IDisposable
    {
        List<Code> GetCodes();
        string FindCode(string code);
        void GenerateCode();
        void RemoveCode(Code code);
    }
}
