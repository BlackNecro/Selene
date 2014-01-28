using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSPVessel = global::Vessel;
using Selene;
using Selene.DataTypes;
using NLua;
using SVector = Selene.DataTypes.Vector;

namespace SeleneKSP.DataTypes
{    
    class Vessel : IVessel
    {
        KSPVessel vessel;

        public Vessel(KSPVessel toUse)
        {
            vessel = toUse;
        }

        public double GetRadarHeight()
        {
            return vessel.heightFromTerrain;
        }

        public double GetHeight()
        {
            return vessel.heightFromSurface;
        }

        public double GetOrbitSpeed()
        {
            return vessel.obt_speed;
        }
        public SVector GetOrbitVelocity()
        {
            return new SVector(vessel.obt_velocity);
        }

        public double GetSurfaceSpeed()
        {
            return vessel.srfSpeed;
        }

        public SVector GetSurfaceVelocity()
        {
            return new SVector(vessel.srf_velocity);
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
            return vessel.GetTotalMass();
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
            throw new NotImplementedException();
        }

        public SVector GetPosition()
        {
            throw new NotImplementedException();
        }

        public UnityEngine.Quaternion GetRotation()
        {
            UnityEngine.Quaternion copy = vessel.transform.rotation;            
            return copy;
        }

        public string GetName()
        {
            return vessel.GetName(); 
        }

        public IControls GetControls()
        {
            return new Controls(vessel); 
        }
    }
}
