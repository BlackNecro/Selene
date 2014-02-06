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


        public Vector3d GetTranslation()
        {
            return new Vector3d(ctrlstate.X, ctrlstate.Z, ctrlstate.Y);
        }

        public void SetTranslation(Vector3d newValue)
        {
            ctrlstate.X = (float)newValue.x;
            ctrlstate.Y = (float)newValue.z;
            ctrlstate.Z = (float)newValue.y;
        }

        public Vector3d GetRotation()
        {
            return new Vector3d(ctrlstate.pitch, ctrlstate.yaw, ctrlstate.roll);
        }

        public void SetRotation(Vector3d newValue)
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
