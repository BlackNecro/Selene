using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;
using NLua;
using KManeuverNode = global::ManeuverNode;
using KVessel = global::Vessel;

namespace SeleneKSP.DataTypes
{
    class FlightPlan : IFlightPlan
    {
        PatchedConicSolver solver;
        KVessel vessel;
        KSPDataProvider dataProvider;

        public FlightPlan(PatchedConicSolver s, KVessel v, KSPDataProvider prov)
        {
            solver = s;
            vessel = v;
            dataProvider = prov;
        }

        public IManeuverNode Add(double ut)
        {
            KManeuverNode n = solver.AddManeuverNode(ut);
            if (n != null)
            {
                solver.UpdateFlightPlan();
                return new ManeuverNode(n, vessel, dataProvider);
            }
            return null;
        }
        public IManeuverNode Add(double ut, SeleneVector dv)
        {
            KManeuverNode n = solver.AddManeuverNode(ut);
            if (n != null)
            {
                ManeuverNode node = new ManeuverNode(n, vessel, dataProvider);
                node.SetDeltaV(dv);
                return node;
            }
            return null;
        }
        public void Remove(IManeuverNode node)
        {
            ManeuverNode snode = (ManeuverNode)node;
            solver.RemoveManeuverNode(snode.GetKspNode());
            solver.UpdateFlightPlan();
        }

        public IManeuverNode GetNextNode()
        {
            if (!solver.maneuverNodes.Any())
                return null;
            else
                return new ManeuverNode(solver.maneuverNodes[0], vessel, dataProvider);
        }
        public IOrbitInfo GetNextEncounter()
        {
            foreach (Orbit patch in solver.flightPlan)
            {
                if (patch.patchStartTransition == Orbit.PatchTransitionType.ENCOUNTER)
                    return new OrbitInfo(dataProvider, patch);
            }
            return null;
        }

        public LuaTable GetNodes()
        {
            LuaTable toReturn = dataProvider.GetNewTable();
            int num = 1;
            foreach (KManeuverNode n in solver.maneuverNodes)
            {
                ManeuverNode node = new ManeuverNode(n, vessel, dataProvider);
                toReturn[num++] = node;
            }
            return toReturn;
        }
        public LuaTable GetOrbits()
        {
            LuaTable toReturn = dataProvider.GetNewTable();
            int num = 1;
            foreach (Orbit o in solver.flightPlan)
            {
                OrbitInfo orbit = new OrbitInfo(dataProvider, o);
                toReturn[num++] = orbit;
            }
            return toReturn;
        }
    }
}
