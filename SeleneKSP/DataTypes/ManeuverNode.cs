using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;
using KManeuverNode = global::ManeuverNode;
using KVessel = global::Vessel;

namespace SeleneKSP.DataTypes
{
    class ManeuverNode : IManeuverNode
    {
        KManeuverNode node;
        KVessel vessel;
        KSPDataProvider dataProvider;

        public ManeuverNode(KManeuverNode n, KVessel v, KSPDataProvider prov)
        {
            node = n;
            vessel = v;
            dataProvider = prov;
        }

        public void SetTime(double ut)
        {
            node.UT = ut;
            Update();
        }
        public double GetTime()
        {
            return node.UT;
        }
        public double time
        {
            get { return GetTime(); }
        }

        public void SetTimeTo(double dt)
        {
            SetTime(Planetarium.GetUniversalTime() + dt);
        }
        public double GetTimeTo()
        {
            return GetTime() - Planetarium.GetUniversalTime();
        }
        public double timeTo
        {
            get { return GetTimeTo(); }
        }

        public void SetRadial(double v)
        {
            node.DeltaV.x = v;
            Update();
        }
        public double GetRadial()
        {
            return node.DeltaV.x;
        }
        public double radial
        {
            get { return GetRadial(); }
        }

        public void SetNormal(double v)
        {
            node.DeltaV.y = v;
            Update();
        }
        public double GetNormal()
        {
            return node.DeltaV.y;
        }
        public double normal
        {
            get { return GetNormal(); }
        }

        public void SetPrograde(double v)
        {
            node.DeltaV.z = v;
            Update();
        }
        public double GetPrograde()
        {
            return node.DeltaV.z;
        }
        public double prograde
        {
            get { return GetPrograde(); }
        }

        public void SetDeltaV(Vector3d dv)
        {
            node.DeltaV = dv;
            Update();
        }
        public Vector3d GetDeltaV()
        {
            return node.DeltaV;
        }
        public Vector3d deltaV
        {
            get { return GetDeltaV(); }
        }

        public Vector3d GetBurnVector()
        {
            return node.GetBurnVector(vessel.orbit);
        }
        public Vector3d burnVector
        {
            get { return GetBurnVector(); }
        }

        public IOrbitInfo GetResultingOrbit()
        {
            return new OrbitInfo(dataProvider, node.nextPatch);
        }
        public IOrbitInfo resultingOrbit
        {
            get { return GetResultingOrbit(); }
        }

        protected void Update()
        {
            node.solver.UpdateFlightPlan();
        }
        public KManeuverNode GetKspNode()
        {
            return node;
        }
    }
}
