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
        FlightCtrlState ctrlstate;

        public Controls(FlightCtrlState newState)
        {
            ctrlstate = newState;            
        }


        public SVector GetTranslation()
        {
            return new SVector(ctrlstate.X, ctrlstate.Z, ctrlstate.Y);
        }

        public void SetTranslation(SVector newValue)
        {
            ctrlstate.X = (float)newValue.x;
            ctrlstate.Y = (float)newValue.z;
            ctrlstate.Z = (float)newValue.y;
        }

        public SVector GetRotation()
        {
            return new SVector(ctrlstate.pitch, ctrlstate.yaw, ctrlstate.roll);
        }

        public void SetRotation(SVector newValue)
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
    }
}
