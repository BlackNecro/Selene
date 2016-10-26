using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using Selene;
using Selene.DataTypes;
//using SVector = Selene.DataTypes.SeleneVector;

namespace SeleneDebugger.DebugImplementations.DataTypes
{
    class Vessel : IVessel
    {
        Lua luaState;

        public Vessel(Lua state)
        {
            luaState = state;
        }

        //public SVector GetPosition()
        //{
        //    return new SVector(0, 0, 0);
        //}

        public UnityEngine.Quaternion GetRotation()
        {
            return new UnityEngine.Quaternion();
        }

        public double GetRadarHeight()
        {
            return 9000;
        }

        public double GetHeight()
        {
            return 9042;
        }

        public double GetOrbitSpeed()
        {
            throw new NotImplementedException();
        }

        public double GetSurfaceSpeed()
        {
            throw new NotImplementedException();
        }

        public ICelestialBody GetParentBody()
        {
            throw new NotImplementedException();
        }

        public double GetDryMass()
        {
            throw new NotImplementedException();
        }

        public double GetMass()
        {
            throw new NotImplementedException();
        }

        //public SVector GetCenterOfMass()
        //{
        //    throw new NotImplementedException();
        //}

        //public SVector GetCenterOfDryMass()
        //{
        //    throw new NotImplementedException();
        //}

        //public SVector GetMomentOfInertia()
        //{
        //    throw new NotImplementedException();
        //}

        public LuaTable GetEngines()
        {
            LuaTable toReturn = (LuaTable)luaState.DoString("return {}")[0];
            toReturn[1] = new Engine();
            toReturn[2] = new Engine();

            return toReturn;
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetRadarAltitude()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetAltitude()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetOrbitSpeed()
        {
            throw new NotImplementedException();
        }

        Vector3d IVessel.GetOrbitVelocity()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetSurfaceSpeed()
        {
            throw new NotImplementedException();
        }

        Vector3d IVessel.GetSurfaceVelocity()
        {
            throw new NotImplementedException();
        }

        ICelestialBody IVessel.GetParentBody()
        {
            throw new NotImplementedException();
        }

        IOrbitInfo IVessel.GetOrbit()
        {
            throw new NotImplementedException();
        }

        IFlightPlan IVessel.GetFlightPlan()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetDryMass()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetMass()
        {
            throw new NotImplementedException();
        }

        Vector3d IVessel.GetCenterOfMass()
        {
            throw new NotImplementedException();
        }

        Vector3d IVessel.GetCenterOfDryMass()
        {
            throw new NotImplementedException();
        }

        Vector3d IVessel.GetMomentOfInertia()
        {
            throw new NotImplementedException();
        }

        LuaTable IVessel.GetEngines()
        {
            throw new NotImplementedException();
        }

        Vector3d ITarget.GetPosition()
        {
            throw new NotImplementedException();
        }

        UnityEngine.QuaternionD ITarget.GetRotation()
        {
            throw new NotImplementedException();
        }

        string ITarget.GetName()
        {
            return "testVessel";
        }

        IPartInfoGroup IVessel.GetParts()
        {
            throw new NotImplementedException();
        }

        IControls IVessel.GetLastControls()
        {
            throw new NotImplementedException();
        }

        int IVessel.GetCurrentStage()
        {
            throw new NotImplementedException();
        }

        int IVessel.GetStageCount()
        {
            throw new NotImplementedException();
        }

        IPartInfoGroup IVessel.GetPartsByStage(int stageNum)
        {
            throw new NotImplementedException();
        }

        IPartInfoGroup IVessel.GetPartsByDecoupleStage(int stageNum)
        {
            throw new NotImplementedException();
        }

        public Vector3d GetAngularVelocity()
        {
            throw new NotImplementedException();
        }

        public double GetApoapsis()
        {
            throw new NotImplementedException();
        }

        public double GetPeriapsis()
        {
            throw new NotImplementedException();
        }

        public UnityEngine.QuaternionD GetSurfaceRotation()
        {
            throw new NotImplementedException();
        }

        public UnityEngine.QuaternionD GetSurfaceRelativeRotation()
        {
            throw new NotImplementedException();
        }

        public Vector3d WorldToLocal(Vector3d toTransform)
        {
            throw new NotImplementedException();
        }

        public Vector3d LocalToWorld(Vector3d toTransform)
        {
            throw new NotImplementedException();
        }
    }
}
