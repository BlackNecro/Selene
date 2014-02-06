using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IVessel : ITarget
    {
        #region Positional Info
        double GetRadarHeight();
        double GetNormalHegiht();
        double GetOrbitSpeed();
        Vector3d GetOrbitVelocity(); 
        double GetSurfaceSpeed();
        Vector3d GetSurfaceVelocity();
        Vector3d GetAngularVelocity();

        double GetApoapsis();
        double GetPeriapsis();
        UnityEngine.QuaternionD GetSurfaceRelativeRotation();
        ICelestialBody GetParentBody();
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
        #endregion
        IControls GetLastControls();

    }
}
