using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Selene.DataTypes
{
    public interface IVessel : ITarget
    {
        #region Positional Info
        double GetRadarAltitude();
        double GetAltitude();
        double GetOrbitSpeed();
        Vector3d GetOrbitVelocity(); 
        double GetSurfaceSpeed();
        Vector3d GetSurfaceVelocity();
        Vector3d GetAngularVelocity();

        double GetApoapsis();
        double GetPeriapsis();
        QuaternionD GetSurfaceRelativeRotation();
        QuaternionD GetSurfaceRotation();
        ICelestialBody GetParentBody();
        IOrbitInfo GetOrbit();
        IFlightPlan GetFlightPlan();
        #endregion
        #region Vessel properties
        double GetDryMass();
        double GetMass();
        Vector3d GetCenterOfMass();
        Vector3d GetCenterOfDryMass();
        Vector3d GetMomentOfInertia();
        #endregion
        #region Part Info
        NLua.LuaTable GetEngines();
        IPartInfoGroup GetParts();
        #endregion
        IControls GetLastControls();

        int GetCurrentStage();

        int GetStageCount();

        IPartInfoGroup GetPartsByStage(int stageNum);
        IPartInfoGroup GetPartsByDecoupleStage(int stageNum);
    }
}
