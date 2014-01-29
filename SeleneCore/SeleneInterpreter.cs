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
            public SeleneCallback(LuaFunction callback)
            {
                Callback = callback;
                CallDelay = 0;
                CallCounter = 0;
                LastCallUT = 0;
            }
            public LuaFunction Callback;
            public int CallDelay;
            public int CallCounter;
            public double LastCallUT;
        }

        List<List<SeleneCallback>> Callbacks = new List<List<SeleneCallback>>();

        public void RegisterCallback(CallbackType type, LuaFunction callback)
        {
            Callbacks[(int)type].Add(new SeleneCallback(callback));
        }

        private void ExecuteCallback(SeleneCallback toCall, object[] parameters)
        {
            if (++toCall.CallCounter > toCall.CallDelay)
            {
                toCall.CallCounter = 0;
                double newTime = dataProvider.GetUniverseTime();
                if (toCall.LastCallUT == 0)
                {
                    toCall.LastCallUT = newTime;
                }
                double delta = newTime - toCall.LastCallUT;
                toCall.LastCallUT = newTime;

                object[] newParameters = new object[parameters.Length + 1];
                parameters.CopyTo(newParameters, 0);
                newParameters[parameters.Length] = (object)delta;
                try
                {
                    object[] ret = toCall.Callback.Call(newParameters);
                    if (ret != null && ret.Length > 0)
                    {
                        if (ret[0] is double)
                        {
                            toCall.CallDelay = (int)((double)ret[0]);
                        }
                    }
                }
                catch (NLua.Exceptions.LuaException ex)
                {
                    dataProvider.Log(String.Format("Error Executing Callback: {0}", ex.Message));
                }
            }
        }

        public void OnFlyByWire(FlightCtrlState newState)
        {
            Selene.DataTypes.IControls luaControlObject = dataProvider.CreateControlState(newState);            
            foreach(var callback in Callbacks[(int)CallbackType.Control])
            {
                object[] parameters = { (object)dataProvider, (object)luaControlObject };
                ExecuteCallback(callback, parameters);
            }
        }


        public SeleneInterpreter(IDataProvider provider)
        {
            foreach(var type in Enum.GetValues(typeof(CallbackType)))
            {
                Callbacks.Add(new List<SeleneCallback>());
            }
            dataProvider = provider;
            provider.RegisterCallbackEvent(RegisterCallback);
            luaState = provider.GetLuaState();
       
        }
        public bool CreateProcess(string file)
        {
            return true;
        }

        public void CleanupProcess()
        {
            foreach(var callbackList in Callbacks)
            {
                callbackList.Clear();
            }
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
            foreach(var callback in Callbacks[(int)CallbackType.Tick])
            {
                ExecuteCallback(callback, new object[] { (object)dataProvider });
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
