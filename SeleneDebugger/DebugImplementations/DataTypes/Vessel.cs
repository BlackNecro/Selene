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
    }
}
