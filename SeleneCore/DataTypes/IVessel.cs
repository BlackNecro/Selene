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
        SeleneVector GetOrbitVelocity(); 
        double GetSurfaceSpeed();
        SeleneVector GetSurfaceVelocity();
        SeleneVector GetAngularVelocity();

        double GetApoapsis();
        double GetPeriapsis();
        UnityEngine.QuaternionD GetSurfaceRelativeRotation();
        ICelestialBody GetParentBody();
        #endregion
        #region Vessel properties
        double GetDryMass();
        double GetMass();
        SeleneVector GetCenterOfMass();
        SeleneVector GetCenterOfDryMass();
        SeleneVector GetMomentOfInertia();
        #endregion
        #region Part Info
        NLua.LuaTable GetEngines();
        #endregion
        IControls GetLastControls();

    }
}
