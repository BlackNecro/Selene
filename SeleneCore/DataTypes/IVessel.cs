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
        Vector GetOrbitVelocity(); 
        double GetSurfaceSpeed();
        Vector GetSurfaceVelocity();
        Vector GetAngularVelocity();

        double GetApoapsis();
        double GetPeriapsis();


        UnityEngine.Quaternion GetSurfaceRelativeRotation();
        ICelestialBody GetParentBody();
        #endregion
        #region Vessel properties
        double GetDryMass();
        double GetMass();
        Vector GetCenterOfMass();
        Vector GetCenterOfDryMass();
        Vector GetMomentOfInertia();
        #endregion
        #region Part Info
        NLua.LuaTable GetEngines();
        #endregion
        IControls GetLastControls();
    }
}
