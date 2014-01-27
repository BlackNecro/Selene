using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSPVessel = Vessel;
using Selene;
using Selene.DataTypes;
using SVector = Selene.DataTypes.Vector;

namespace SeleneKSP.DataTypes
{
    class Controls : IControls
    {
        KSPVessel vessel;

        public Controls(KSPVessel parentVessel)
        {
            vessel = parentVessel;            
        }
        

        SVector IControls.GetTranslational()
        {
            var ctrls = vessel.ctrlState;
            return new SVector(ctrls.X, ctrls.Y, ctrls.Z);
        }

        void IControls.SetTranslational(SVector newValue)
        {
            var ctrls = vessel.ctrlState;
            ctrls.X = (float)newValue.x;
            ctrls.Y = (float)newValue.y;
            ctrls.Z = (float)newValue.z;
        }

        SVector IControls.GetRotational()
        {
            var ctrls = vessel.ctrlState;
            return new SVector(ctrls.pitch, ctrls.yaw, ctrls.roll);
        }

        void IControls.SetRotational(SVector newValue)
        {
            var ctrls = vessel.ctrlState;
            ctrls.pitch = (float)newValue.x;
            ctrls.yaw = (float)newValue.y;
            ctrls.roll = (float)newValue.z;
        }

        double IControls.GetThrust()
        {
            var ctrls = vessel.ctrlState;
            return ctrls.mainThrottle;
        }

        void IControls.SetThrust(double newValue)
        {
            var ctrls = vessel.ctrlState;
            ctrls.mainThrottle = (float)newValue;
        }
    }
}
