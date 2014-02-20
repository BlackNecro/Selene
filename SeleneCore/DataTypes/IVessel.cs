using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IVessel : ITarget
    {
        #region Positional Info
        double GetRadarAltitude();
        double GetAltitude();
        double GetOrbitSpeed();
        SeleneVector GetOrbitVelocity(); 
        double GetSurfaceSpeed();
        SeleneVector GetSurfaceVelocity();
        SeleneVector GetAngularVelocity();

        double GetApoapsis();
        double GetPeriapsis();
        SeleneQuaternion GetSurfaceRelativeRotation();
        SeleneQuaternion GetSurfaceRotation();
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
        IPartInfoGroup GetParts();
        #endregion
        IControls GetLastControls();

        int GetCurrentStage();

        int GetStageCount();

        IPartInfoGroup GetPartsByStage(int stageNum);

    }
}
