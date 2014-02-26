using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene.DataTypes
{
    public interface IPartInfoGroup
    {
        IPartInfoGroup GetPartsByModule(string moduleName);
        LuaTable GetPartTable();    
        bool Action(string actionName, bool activate);
        bool Event(string eventName);
        LuaTable GetActions();
        LuaTable GetEvents();
        void Activate();

        LuaTable GetResources();
        double GetResourceCapacity(string name);
        double GetResourceAmount(string name);


    }
}
