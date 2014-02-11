using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPart = global::Part;
using KSPVessel = global::Vessel;
using Selene;
using Selene.DataTypes;
using NLua;

namespace SeleneKSP.DataTypes
{
    class Vessel : IVessel
    {
        KSPVessel vessel;
        KSPDataProvider provider;

        public Vessel(KSPVessel toUse, KSPDataProvider prov)
        {
            vessel = toUse;
            provider = prov;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vessel)
            {
                return ((Vessel)obj).vessel == this.vessel;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return vessel.GetHashCode();
        }

        public double GetRadarHeight()
        {
            return vessel.heightFromTerrain;
        }

        public double GetNormalHegiht()
        {
            return vessel.heightFromSurface;
        }

        public double GetOrbitSpeed()
        {
            return vessel.obt_speed;
        }
        public SeleneVector GetOrbitVelocity()
        {
            return new SeleneVector(vessel.obt_velocity);
        }

        public double GetSurfaceSpeed()
        {
            return vessel.srfSpeed;
        }

        public SeleneVector GetSurfaceVelocity()
        {
            return new SeleneVector(vessel.srf_velocity);
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

        public SeleneVector GetCenterOfMass()
        {
            return new SeleneVector(vessel.findLocalCenterOfMass());
        }

        public SeleneVector GetCenterOfDryMass()
        {
            throw new NotImplementedException();
        }

        public SeleneVector GetMomentOfInertia()
        {
            throw new NotImplementedException();
        }

        public LuaTable GetEngines()
        {
            LuaTable tab = provider.GetNewTable();
            int index = 1;
            foreach (KPart part in vessel.Parts)
            {
                foreach (ModuleEngines engine in part.FindModulesImplementing<ModuleEngines>())
                {
                    EngineInfo info = new EngineInfo(part, engine);
                    tab[index++] = info;
                }
            }
            return tab;
        }

        public SeleneVector GetPosition()
        {
            return new SeleneVector(vessel.GetWorldPos3D());
        }

        public UnityEngine.QuaternionD GetRotation()
        {
            return vessel.transform.rotation;
        }

        public string GetName()
        {
            return vessel.GetName();
        }

        public IControls GetLastControls()
        {
            return new Controls(vessel.ctrlState);
        }


        public UnityEngine.QuaternionD GetSurfaceRelativeRotation()
        {
            return vessel.srfRelRotation;
        }

        public SeleneVector GetAngularVelocity()
        {
            return new SeleneVector(vessel.angularVelocity);
        }



        public double GetApoapsis()
        {
            return vessel.orbit.ApA;
        }

        public double GetPeriapsis()
        {
            return vessel.orbit.PeA;
        }


        public SeleneVector WorldToLocal(SeleneVector toTransform)
        {

            var config = KSP.IO.PluginConfiguration.CreateForType<SeleneInterpreter>();

            UnityEngine.QuaternionD test = vessel.transform.rotation;

            return new SeleneVector(((UnityEngine.QuaternionD)vessel.transform.rotation) * toTransform);
        }

        public SeleneVector LocalToWorld(SeleneVector toTransform)
        {
            return new SeleneVector( UnityEngine.QuaternionD.Inverse((UnityEngine.QuaternionD)vessel.transform.rotation) * toTransform);
        }
    }
}
