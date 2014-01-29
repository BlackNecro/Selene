using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene
{

    public delegate void RegisterCallback(CallbackType type, LuaFunction callback);
    public enum CallbackType
    {
        Tick,
        Control,
        GUI
    }
}
