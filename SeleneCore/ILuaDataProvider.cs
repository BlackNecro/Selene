using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;
using NLua;

namespace Selene
{    
    public interface ILuaDataProvider
    {
        double GetUniverseTime();
        IVessel GetExecutingVessel();
        IVessel GetCommandedVessel();
        LuaTable GetVessels();
        ITarget GetCurrentTarget();
        ICelestialBody GetCelestialBody(string name);
        IManeuverNode GetNextManeuverNode();
        LuaTable GetManeuverNodes();
        IManeuverNode CreateNewManeuverNode();
        string ReadFile(string path);
        bool AppendFile(string path, string content);
        bool WriteFile(string path, string content);
        IControls CreateControlState(FlightCtrlState toCreate);

        void AdvanceStage();

    }
}
