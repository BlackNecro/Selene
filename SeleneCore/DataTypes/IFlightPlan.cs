using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene.DataTypes
{
    public interface IFlightPlan
    {
        IManeuverNode Add(double ut);
        IManeuverNode Add(double ut, Vector3d dv);
        void Remove(IManeuverNode node);

        IManeuverNode GetNextNode();
        IOrbitInfo GetNextEncounter();

        LuaTable GetNodes();
        LuaTable GetOrbits();
    }
}
