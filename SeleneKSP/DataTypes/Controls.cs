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


        public SVector GetTranslational()
        {
            var ctrls = vessel.ctrlState;
            return new SVector(ctrls.X, ctrls.Y, ctrls.Z);
        }

        public void SetTranslational(SVector newValue)
        {
            var ctrls = vessel.ctrlState;
            ctrls.X = (float)newValue.x;
            ctrls.Y = (float)newValue.y;
            ctrls.Z = (float)newValue.z;
        }

        public SVector GetRotational()
        {
            var ctrls = vessel.ctrlState;
            return new SVector(ctrls.pitch, ctrls.yaw, ctrls.roll);
        }

        public void SetRotational(SVector newValue)
        {
            var ctrls = vessel.ctrlState;
            ctrls.pitch = (float)newValue.x;
            ctrls.yaw = (float)newValue.y;
            ctrls.roll = (float)newValue.z;
        }

        public double GetThrottle()
        {
            var ctrls = vessel.ctrlState;
            return ctrls.mainThrottle;
        }

        public void SetThrottle(double newValue)
        {
            var ctrls = vessel.ctrlState;
            ctrls.mainThrottle = (float)newValue;
        }
    }
}
