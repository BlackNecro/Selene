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
        object Field(string fieldName);

        bool Action(string actionName, bool activate);

        bool Event(string eventName);

        
        LuaTable ListActions();
        LuaTable ListEvents();
        LuaTable ListFields();


    }
}
