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

        List<LuaFunction> processes = new List<LuaFunction>();
        int currentProcess = 0;
        LuaFunction dispatcher;
        

        public SeleneInterpreter(IDataProvider provider)
        {
            dataProvider = provider;
            luaState = provider.GetLuaState();
            
            dispatcher = luaState.LoadString(@"
local args = {...} 
print('building instance')
local co = coroutine.create(args[1]) 
coroutine.resume(co,Selene)
return function()
    print('Called the instance')
    local rets = {coroutine.resume(co)}
    for k,v in pairs(rets) do
        print(k,v) 
    end
    if not coroutine.resume(co) then
        return true
    end
    return false
end
", "Process Dispatcher");            
        }
        public bool CreateProcess(string file)
        {
            UnityEngine.Debug.Log("dispatcher");
            UnityEngine.Debug.Log(dispatcher);
            UnityEngine.Debug.Log("Creating Process for " + file);
            string input = KSP.IO.File.ReadAllText<SeleneInterpreter>(file);
            UnityEngine.Debug.Log(input);
            LuaFunction loadedProcess = luaState.LoadString(input, String.Format("Process {0}: {1}",processes.Count + 1,file));
            UnityEngine.Debug.Log("parsed file");
            object[] parameter = { (object)loadedProcess };
            UnityEngine.Debug.Log("creating instance");
            LuaFunction processInstance = (LuaFunction)dispatcher.Call(parameter)[0];
            UnityEngine.Debug.Log("Adding to list");
            processes.Add(processInstance);
            UnityEngine.Debug.Log("done");            
            return true;
        }

        public bool CreateProcess(string[] lines, string filename)
        {
            string input = String.Join("\n", lines);
            LuaFunction loadedProcess = luaState.LoadString(input, String.Format("Process {0}: {1}", processes.Count + 1, filename));
            object[] parameter = { (object)loadedProcess };
            LuaFunction processInstance = (LuaFunction)dispatcher.Call(parameter)[0];
            processes.Add(processInstance);
            return true;
        }

        public void ExecuteProcess()
        {            
            if(processes.Count > 0)
            {                
                object[] returned = processes[currentProcess].Call();
                bool cont = (bool)returned[0];
                if(!cont)
                {                    
                    processes.RemoveAt(currentProcess--);                    
                }
                currentProcess++;
                if(currentProcess == processes.Count)
                {
                    currentProcess = 0;
                }
            }
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
