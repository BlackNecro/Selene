using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene
{
    public interface IInterpreter
    {
        bool CreateProcess(string file);
        bool CreateProcess(string[] lines, string filename);
        void ExecuteProcess();
        bool HasVariable(string name);
        void MemoryDump();
        string GetInterpreterVersion();
    }
}
