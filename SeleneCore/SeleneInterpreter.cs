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

        public void OnFlyByWire(FlightCtrlState newState)
        {
            Selene.DataTypes.IControls luaControlObject = ((ILuaDataProvider)luaState).CreateControlState(newState);
            RootProcess.Execute(CallbackType.Control, luaControlObject);
        }


        public SeleneInterpreter(SeleneLuaState provider)
        {
            luaState = provider;
            RootProcess = new SeleneProcess(provider);
            RootProcess.name = "Root";
            RootProcess.Active = true;
        }
        public bool CreateProcess(string file)
        {
            SeleneProcess newProcess = new SeleneProcess(luaState);
            if (newProcess.LoadFromFile(file))
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
            if (newProcess.LoadFromString(String.Join("\n", lines), filename))
            {
                RootProcess.AddChildProcess(newProcess);
                newProcess.Active = true;
                return true;
            }
            return false;
        }
        public void ExecuteProcess()
        {
            RootProcess.Execute(CallbackType.Tick);
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
            return "Selene v1 Lua 5.1";
        }

        public void SaveState(ConfigNode saveInto)
        {
            saveInto.ClearData();
            saveInto.AddValue("Active", RootProcess.Active.ToString());
            foreach (var Proc in RootProcess.Children)
            {
                Proc.Execute(CallbackType.Save);
                Proc.SaveObject(saveInto);
            }
        }

        public void LoadState(ConfigNode loadFrom)
        {
            if (loadFrom.HasValue("Active"))
            {
                RootProcess.Active = Boolean.Parse(loadFrom.GetValue("Active"));
            }
            foreach (ConfigNode procNode in loadFrom.GetNodes("Process"))
            {
                SeleneProcess newProc = RootProcess.CreateChildProcess();
                newProc.LoadState(procNode);
                newProc.Execute(CallbackType.Load);
            }

        }

        public void PhysicsUpdate()
        {
            RootProcess.Execute(CallbackType.PhysicsUpdate);
        }
    }
}
