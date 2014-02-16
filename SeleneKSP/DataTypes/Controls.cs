using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSPVessel = Vessel;
using Selene;
using Selene.DataTypes;

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
            ctrlstate.X = (float)newValue.x;
            ctrlstate.Y = (float)newValue.z;
            ctrlstate.Z = (float)newValue.y;
        }

        public SeleneVector GetRotation()
        {
            return new SeleneVector(ctrlstate.pitch, ctrlstate.yaw, ctrlstate.roll);
        }

        public void SetRotation(SeleneVector newValue)
        {
            ctrlstate.pitch = (float)newValue.x;
            ctrlstate.yaw = (float)newValue.y;
            ctrlstate.roll = (float)newValue.z;
        }

        public double GetThrottle()
        {
            return ctrlstate.mainThrottle;
        }

        public void SetThrottle(double newValue)
        {
            ctrlstate.mainThrottle = (float)newValue;
        }

        public double GetWheelSteer()
        {
            return ctrlstate.wheelSteer;
        }
        public void SetWheelSteer(double newValue)
        {
            ctrlstate.wheelSteer = (float)newValue;
        }

        public double GetWheelThrottle()
        {
            return ctrlstate.wheelThrottle;
        }
        public void SetWheelThrottle(double newValue)
        {
            ctrlstate.wheelThrottle = (float)newValue;
        }
    }
}
