using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene.DataTypes
{
    public interface IPartInfoGroup
    {
        IPartInfoGroup GetPartsByMod(string moduleName);
        LuaTable GetPartTable();    
        bool Action(string actionName, bool activate);
        bool Event(string eventName);
        LuaTable GetActions();
        LuaTable GetEvents();

    }
}
