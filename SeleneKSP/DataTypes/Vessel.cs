using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KPart = global::Part;
using KSPVessel = global::Vessel;
using Selene;
using Selene.DataTypes;
using NLua;
using SeleneKSP.Util;

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

        public double GetRadarAltitude()
        {
            return vessel.heightFromTerrain;
        }

        public double GetAltitude()
        {
            return vessel.altitude;//.heightFromSurface;
        }

        public double GetOrbitSpeed()
        {
            return vessel.obt_speed;
        }
        public Vector3d GetOrbitVelocity()
        {
            return vessel.obt_velocity;
        }

        public double GetSurfaceSpeed()
        {
            return vessel.srfSpeed;
        }

        public Vector3d GetSurfaceVelocity()
        {
            return vessel.srf_velocity;
        }

        public ICelestialBody GetParentBody()
        {
            return new CelestialBody(provider, vessel.mainBody);
        }

        public IOrbitInfo GetOrbit()
        {
            return new OrbitInfo(provider, vessel.orbit);
        }

        public IFlightPlan GetFlightPlan()
        {
            return new FlightPlan(vessel.patchedConicSolver, vessel, provider);
        }

        public double GetDryMass()
        {
            return this.vessel.parts.Where(item => item.physicalSignificance == KPart.PhysicalSignificance.FULL).Sum((item) => item.mass);
        }

        public double GetMass()
        {
            return vessel.GetTotalMass();
        }

        public Vector3d GetCenterOfMass()
        {
            return vessel.localCoM;
        }

        public Vector3d GetCenterOfDryMass()
        {
            var center = new Vector3d();
            var parts = this.vessel.parts.Where(item => item.physicalSignificance == KPart.PhysicalSignificance.FULL);
            center = parts.Aggregate(center, (current, item) => current + (item.orgPos + (item.orgRot * item.CoMOffset)) * item.mass);
            center /= parts.Sum((item) => item.mass);
            return center;
        }

        public Vector3d GetMomentOfInertia()
        {
            //TODO Figure out the maths
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

        public Vector3d GetPosition()
        {
            return vessel.GetWorldPos3D();
        }

        public QuaternionD GetRotation()
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


        public QuaternionD GetSurfaceRelativeRotation()
        {
            // Transform the vessel rotation into the surface reference
            UnityEngine.Quaternion absRotation = GetRotation();
            UnityEngine.Quaternion srfRelRotation = UnityEngine.Quaternion.Inverse(GetSurfaceRotation()) * absRotation;
            return srfRelRotation;
        }
        public QuaternionD GetSurfaceRotation()
        {
            // Adapted from Mechjeb2 VesselState.cs
            Vector3d CoM = vessel.CoM;
            Vector3d up = (CoM - vessel.mainBody.position).normalized;
            Vector3d north = Vector3d.Exclude(up, (vessel.mainBody.position + vessel.mainBody.transform.up * (float)vessel.mainBody.Radius) - CoM).normalized;
            Vector3d east = vessel.mainBody.getRFrmVel(CoM).normalized;
            return UnityEngine.Quaternion.LookRotation(north, up);
        }

        public Vector3d GetAngularVelocity()
        {
            return vessel.angularVelocity;
        }



        public double GetApoapsis()
        {
            return vessel.orbit.ApA;
        }

        public double GetPeriapsis()
        {
            return vessel.orbit.PeA;
        }


        public Vector3d WorldToLocal(Vector3d toTransform)
        {
            return UnityEngine.QuaternionD.Inverse((UnityEngine.QuaternionD)vessel.transform.rotation) * toTransform;
        }

        public Vector3d LocalToWorld(Vector3d toTransform)
        {
            return ((UnityEngine.QuaternionD)vessel.transform.rotation) * toTransform;
        }


        public IPartInfoGroup GetParts()
        {
            return new PartInfoGroup(provider, vessel);
        }


        public int GetCurrentStage()
        {
            return vessel.currentStage;

        }

        public int GetStageCount()
        {
            return vessel.Parts.Max(part => part.inverseStage) + 1;
        }

        public IPartInfoGroup GetPartsByStage(int stageNum)
        {
            PartInfoGroup toReturn = new PartInfoGroup(provider);
            foreach (var part in vessel.Parts.Where(part => part.inverseStage == stageNum))
            {
                toReturn.AddPart(part);
            }
            return toReturn;
        }
        public IPartInfoGroup GetPartsByDecoupleStage(int stageNum)
        {
            PartInfoGroup toReturn = new PartInfoGroup(provider);
            foreach (var part in vessel.Parts.Where(part => part.GetDecoupleStage() == stageNum))
            {
                toReturn.AddPart(part);
            }
            return toReturn;
        }
    }
}
