using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeleneDebugger.DebugImplementations.DataTypes
{
    class Engine : Selene.DataTypes.IEngineInfo
    {
        bool enabled = false;
        double percentage = 0.5;
        public Selene.DataTypes.Vector GetOffset()
        {
            throw new NotImplementedException();
        }

        public bool GetEnabled()
        {
            return enabled;
        }

        public void SetEnabled(bool newValue)
        {
            enabled = newValue;
        }

        public double GetThrottlePercentage()
        {
            return percentage;
        }

        public void SetThrottlePercentage(double newValue)
        {
            percentage = newValue;
        }

        public double GetMaxThrust()
        {
            return 1000;
        }
    }
}
