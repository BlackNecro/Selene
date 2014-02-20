using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene.DataTypes
{
    public interface IPartInfo
    {
        SeleneVector GetOffset();
        SeleneQuaternion GetRotation();
        string GetName();
        string GetTitle();
        double GetMass();

        bool SetField(string fieldName, object value);
        object GetField(string fieldName);

        bool Action(string actionName, bool activate);

        bool Event(string eventName);

        
        LuaTable GetActions();
        LuaTable GetEvents();
        LuaTable GetFields();

        void Activate();

    }
}
