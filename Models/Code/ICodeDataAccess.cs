using System;
using System.Collections.Generic;

namespace LockServerAPI.Models.Code
{
    public interface ICodeDataAccess : IDisposable
    {
        List<Code> GetCodes();
        (string lockId, string config) FindCode(string code);
        void GenerateCode(string lockId, string config);
        void EditCode(Code oldCode, Code newCode);
        void RemoveCode(Code code);
    }
}
