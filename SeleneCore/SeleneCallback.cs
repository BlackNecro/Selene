using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{
    public class SeleneCallback
    {
        public SeleneCallback(LuaFunction callback, SeleneLuaState dataprov, CallbackType type)
        {
            Callback = callback;
            luaState = dataprov;
            CallbackType = type;
            CallDelay = 0;
            CallCounter = 0;
            LastCallUT = 0;
        }
        public LuaFunction Callback;
        public int CallDelay;
        public int CallCounter;
        public double LastCallUT;
        public CallbackType CallbackType;
        SeleneLuaState luaState;


        public bool Execute(object[] parameters)
        {
            if (++CallCounter > CallDelay)
            {
                CallCounter = 0;
                double newTime = luaState.GetUniverseTime();
                if (LastCallUT == 0)
                {
                    LastCallUT = newTime;
                }
                double delta = newTime - LastCallUT;
                LastCallUT = newTime;

                object[] newParameters = new object[parameters.Length + 2];
                parameters.CopyTo(newParameters, 1);
                newParameters[0] = (object)luaState;
                newParameters[newParameters.Length - 1] = (object)delta;
                try
                {
                    object[] ret = Callback.Call(newParameters);
                    if (ret != null && ret.Length > 0)
                    {
                        if (ret[0] is double)
                        {
                            CallDelay = (int)((double)ret[0]);
                        }
                    }
                }
                catch (NLua.Exceptions.LuaException ex)
                {
                    string details = "";
                    Exception curLevel = ex;
                    while (curLevel != null)
                    {
                        details = details + "\n " + curLevel.ToString();
                        curLevel = curLevel.InnerException;
                    }

                    luaState.Log(String.Format("Error Executing Callback:\nDetails:{0}\nTrace:\n{1}", details, ex.StackTrace), 0);
                    return false;
                }                
            }
            return true;
        }
    }


}
