using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;
using Selene;
using Selene.DataTypes;
using SVector = Selene.DataTypes.Vector;

namespace SeleneDebugger.DebugImplementations.DataTypes
{
    class Vessel : IVessel
    {
        Lua luaState;
        
        public Vessel(Lua state)
        {
            luaState = state;
        }


        public SVector GetPosition()
        {
            return new SVector(0, 0, 0);
        }

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

        public SVector GetCenterOfMass()
        {
            throw new NotImplementedException();
        }

        public SVector GetCenterOfDryMass()
        {
            throw new NotImplementedException();
        }

        public SVector GetMomentOfInertia()
        {
            throw new NotImplementedException();
        }

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

        double IVessel.GetRadarHeight()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetHeight()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetOrbitSpeed()
        {
            throw new NotImplementedException();
        }

        SVector IVessel.GetOrbitVelocity()
        {
            throw new NotImplementedException();
        }

        double IVessel.GetSurfaceSpeed()
        {
            throw new NotImplementedException();
        }

        SVector IVessel.GetSurfaceVelocity()
        {
            throw new NotImplementedException();
        }

        ICelestialBody IVessel.GetParentBody()
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

        SVector IVessel.GetCenterOfMass()
        {
            throw new NotImplementedException();
        }

        SVector IVessel.GetCenterOfDryMass()
        {
            throw new NotImplementedException();
        }

        SVector IVessel.GetMomentOfInertia()
        {
            throw new NotImplementedException();
        }

        LuaTable IVessel.GetEngines()
        {
            throw new NotImplementedException();
        }

        SVector ITarget.GetPosition()
        {
            throw new NotImplementedException();
        }

        UnityEngine.Quaternion ITarget.GetRotation()
        {
            throw new NotImplementedException();
        }

        string ITarget.GetName()
        {
            return "testVessel";
        }

        IControls IVessel.GetControls()
        {
            throw new NotImplementedException();
        }
    }
}
