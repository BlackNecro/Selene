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

        private class SeleneCallback
        {
            public SeleneCallback(LuaFunction callback, string name, int delay)
            {
                Callback = callback;
                Name = name;
                Delay = delay;
                Counter = 0;
            }
            public LuaFunction Callback;
            public string Name;
            public int Delay;
            public int Counter;
        }

        List<SortedDictionary<string, SeleneCallback>> Callbacks = new List<SortedDictionary<string, SeleneCallback>>();

        public void RegisterTickCallback(CallbackType type, LuaFunction callback, string name, int Delay)
        {
            Callbacks[(int)type][name] = new SeleneCallback(callback, name, Delay);
        }


        public SeleneInterpreter(IDataProvider provider)
        {
            foreach(var type in Enum.GetValues(typeof(CallbackType)))
            {
                Callbacks.Add(new SortedDictionary<string, SeleneCallback>());
            }
            dataProvider = provider;
            provider.RegisterCallbackEvent(RegisterTickCallback);
            luaState = provider.GetLuaState();
       
        }
        public bool CreateProcess(string file)
        {
            return true;
        }

        public bool CreateProcess(string[] lines, string filename)
        {
            string input = String.Join("\n", lines);
            try
            {
                luaState.DoString(input);
                return true;
            }
            catch (NLua.Exceptions.LuaException ex)
            {
                dataProvider.Log(String.Format("Failed to Create Process {0} Lua Error {1}", filename, ex.Message));                
            }
            return false;
        }

        public void ExecuteProcess()
        {         
            foreach(var kv in Callbacks[(int)CallbackType.Tick])
            {
                SeleneCallback callback = kv.Value;
                if (++callback.Counter >= kv.Value.Delay)
                {
                    callback.Counter = 0;
                    try
                    {
                        callback.Callback.Call(new object[] { (object)callback.Name });  
                    } catch (NLua.Exceptions.LuaException ex)
                    {
                        dataProvider.Log(String.Format("Error Executing {0}", ex.Message));
                    }
                    
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
