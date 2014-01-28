using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{

    public delegate void RegisterCallback(CallbackType type, LuaFunction callback, string name, int delay);
    public enum CallbackType
    {
        Tick,
        GUI
    }
}
