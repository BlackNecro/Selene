using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSPVessel = Vessel;
using Selene;
using Selene.DataTypes;
using Util = Selene.Util;


namespace SeleneKSP.DataTypes
{
    class Controls : IControls
    {
        FlightCtrlState ctrlstate;

        public Controls(FlightCtrlState newState)
        {
            ctrlstate = newState;            
        }


        public SeleneVector GetTranslation()
        {
            return new SeleneVector(ctrlstate.X, ctrlstate.Z, ctrlstate.Y);
        }

        public void SetTranslation(SeleneVector newValue)
        {
            SeleneVector clamped = Util.Math.Clamp(newValue, -1.0, 1.0);
            ctrlstate.X = (float)clamped.x;
            ctrlstate.Y = (float)clamped.z;
            ctrlstate.Z = (float)clamped.y;
        }

        public SeleneVector GetRotation()
        {
            return new SeleneVector(ctrlstate.pitch, ctrlstate.yaw, ctrlstate.roll);
        }

        public void SetRotation(SeleneVector newValue)
        {
            SeleneVector clamped = Util.Math.Clamp(newValue, -1.0, 1.0);
            ctrlstate.pitch = (float)clamped.x;
            ctrlstate.yaw = (float)clamped.y;
            ctrlstate.roll = (float)clamped.z;
        }

        public double GetThrottle()
        {
            return ctrlstate.mainThrottle;
        }

        public void SetThrottle(double newValue)
        {
            ctrlstate.mainThrottle = (float)Util.Math.Clamp(newValue, 0.0, 1.0); ;
        }

        public double GetWheelSteer()
        {
            return ctrlstate.wheelSteer;
        }
        public void SetWheelSteer(double newValue)
        {
            ctrlstate.wheelSteer = (float)Util.Math.Clamp(newValue, -1.0, 1.0);
        }

        public double GetWheelThrottle()
        {
            return ctrlstate.wheelThrottle;
        }
        public void SetWheelThrottle(double newValue)
        {
            ctrlstate.wheelThrottle = (float)Util.Math.Clamp(newValue, -1.0, 1.0);
        }
    }
}
