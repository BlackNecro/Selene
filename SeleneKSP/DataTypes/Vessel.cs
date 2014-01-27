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

        double IVessel.GetRadarHeight()
        {
            return vessel.heightFromTerrain;
        }

        double IVessel.GetHeight()
        {
            return vessel.heightFromSurface;
        }

        double IVessel.GetOrbitSpeed()
        {
            return vessel.obt_speed;
        }
        SVector IVessel.GetOrbitVelocity()
        {
            return new SVector(vessel.obt_velocity);
        }

        double IVessel.GetSurfaceSpeed()
        {
            return vessel.srfSpeed;
        }

        SVector IVessel.GetSurfaceVelocity()
        {
            return new SVector(vessel.srf_velocity);
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
            return vessel.GetTotalMass();
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
            UnityEngine.Quaternion copy = vessel.transform.rotation;            
            return copy;
        }

        string ITarget.GetName()
        {
            return vessel.GetName(); 
        }

        IControls IVessel.GetControls()
        {
            return new Controls(vessel); 
        }
    }
}
