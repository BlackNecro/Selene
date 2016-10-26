using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Selene.DataTypes;
using UnityEngine;

namespace SeleneDebugger.DebugImplementations.DataTypes
{
    class Engine : Selene.DataTypes.IEngineInfo
    {
        bool enabled = false;
        double percentage = 0.5;

        public bool GetEnabled()
        {
            return enabled;
        }

        public void SetEnabled(bool newValue)
        {
            enabled = newValue;
        }

        public double GetThrustPercentage()
        {
            return percentage;
        }

        public void SetThrustPercentage(double newValue)
        {
            percentage = newValue;
        }

        public double GetMaxThrust()
        {
            return 1000;
        }

        public Vector3d GetOffset()
        {
            throw new NotImplementedException();
        }

        public UnityEngine.QuaternionD GetRotation()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetTitle()
        {
            throw new NotImplementedException();
        }

        public double GetMass()
        {
            throw new NotImplementedException();
        }

        public double GetDryMass()
        {
            throw new NotImplementedException();
        }
    }
}
