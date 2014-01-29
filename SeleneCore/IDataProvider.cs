using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;
using NLua;

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
        void RegisterCallbackEvent(RegisterCallback toCall);
        void RegisterTick(LuaFunction toCall);
        void RegisterControl(LuaFunction toCall);
        void Log(string toLog);

        Selene.DataTypes.IControls CreateControlState(FlightCtrlState toCreate);
        Lua GetLuaState();

        LuaTable GetNewTable();

    }
}
