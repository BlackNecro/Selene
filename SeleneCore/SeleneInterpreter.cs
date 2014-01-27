using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{
    public class SeleneInterpreter : IInterpreter
    {
        IDataProvider dataProvider;
        Lua luaState;
        

        public SeleneInterpreter(IDataProvider provider)
        {
            dataProvider = provider;
            luaState = provider.GetLuaState(); ;
            //provider.LoadLuaClasses(luaState);            
        }
        public bool CreateProcess(string file)
        {
            luaState.DoFile(file);
            return true;
        }

        public bool CreateProcess(string[] lines, string filename)
        {
            throw new NotImplementedException();
        }

        public void ExecuteProcess()
        {
            throw new NotImplementedException();
        }

        public bool HasVariable(string name)
        {
            throw new NotImplementedException();
        }

        public void MemoryDump()
        {
            throw new NotImplementedException();
        }

        public string GetInterpreterVersion()
        {
            return "Selene v1 Lua 5.2";
        }
    }
}
