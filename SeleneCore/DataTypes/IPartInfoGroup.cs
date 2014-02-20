using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene.DataTypes
{
    public interface IPartInfoGroup
    {
        IPartInfoGroup PartsIncludingModule(string moduleName);
        LuaTable GetPartTable();    
        bool Action(string actionName, bool activate);
        bool Event(string eventName);
        LuaTable ListActions();
        LuaTable ListEvents();

    }
}
