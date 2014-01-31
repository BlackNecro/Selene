using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{

    public class SeleneInterpreter : IInterpreter
    {
        SeleneLuaState luaState;

        public SeleneProcess RootProcess;

        //public List<SeleneProcess> Processes = new List<SeleneProcess>();        

        public void OnFlyByWire(FlightCtrlState newState)
        {
            Selene.DataTypes.IControls luaControlObject = ((ILuaDataProvider)luaState).CreateControlState(newState);
            object[] parameters = { (object)luaControlObject };
            RootProcess.Execute(CallbackType.Control,parameters);                       
        }


        public SeleneInterpreter(SeleneLuaState provider)
        {
            luaState = provider;
            RootProcess = new SeleneProcess(provider);
            RootProcess.Active = true;
        }
        public bool CreateProcess(string file)
        {
            SeleneProcess newProcess = new SeleneProcess(luaState);
            if(newProcess.LoadFromFile(file))
            {
                RootProcess.AddChildProcess(newProcess);
                newProcess.Active = true;
                return true;
            }
            return false;
        }

        public bool CreateProcess(string[] lines, string filename)
        {
            SeleneProcess newProcess = new SeleneProcess(luaState);
            if(newProcess.LoadFromString(String.Join("\n", lines),filename))
            {
                RootProcess.AddChildProcess(newProcess);
                newProcess.Active = true;
                return true;
            }
            return false;
        }
        public void ExecuteProcess()
        {                      
            RootProcess.Execute(CallbackType.Tick, new object[]{});
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
