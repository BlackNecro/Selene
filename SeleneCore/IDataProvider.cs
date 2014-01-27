using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;

namespace Selene
{
    public interface IDataProvider
    {
        double GetUniverseTime();
        IVessel GetExecutingVessel();
        IVessel GetCommandedVessel();
        ITarget GetCurrentTarget();
        ICelestialBody GetCelestialBody(string name);
        IManeuverNode GetNextManeuverNode();
        NLua.LuaTable GetManeuverNodes();
        IManeuverNode CreateNewManeuverNode();
        GUI.IButton CreateNewButton(string Name);        
        NLua.Lua GetLuaState();

    }
}
