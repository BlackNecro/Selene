using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSPVessel = Vessel;
using Selene;
using Selene.DataTypes;
using SUtil = Selene.Util;


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
            return new Vector3d(-ctrlstate.X, -ctrlstate.Z, -ctrlstate.Y);
        }                                         

        public void SetTranslation(Vector3d newValue)
        {
            Vector3d clamped = SUtil.Math.Clamp(newValue, -1.0, 1.0);
            ctrlstate.X = (float)-clamped.x;
            ctrlstate.Y = (float)-clamped.z;
            ctrlstate.Z = (float)-clamped.y;
        }

        public Vector3d GetRotation()
        {
            return new Vector3d(-ctrlstate.pitch, -ctrlstate.roll,-ctrlstate.yaw);
        }

        public void SetRotation(Vector3d newValue)
        {
            Vector3d clamped = SUtil.Math.Clamp(newValue, -1.0, 1.0);
            ctrlstate.pitch = (float)-clamped.x;
            ctrlstate.yaw = (float)-clamped.z;
            ctrlstate.roll = (float)-clamped.y;
        }

        public double GetThrottle()
        {
            return ctrlstate.mainThrottle;
        }

        public void SetThrottle(double newValue)
        {
            ctrlstate.mainThrottle = (float)SUtil.Math.Clamp(newValue, 0.0, 1.0); ;
        }

        public double GetWheelSteer()
        {
            return ctrlstate.wheelSteer;
        }
        public void SetWheelSteer(double newValue)
        {
            ctrlstate.wheelSteer = (float)SUtil.Math.Clamp(newValue, -1.0, 1.0);
        }

        public double GetWheelThrottle()
        {
            return ctrlstate.wheelThrottle;
        }
        public void SetWheelThrottle(double newValue)
        {
            ctrlstate.wheelThrottle = (float)SUtil.Math.Clamp(newValue, -1.0, 1.0);
        }
    }
}
