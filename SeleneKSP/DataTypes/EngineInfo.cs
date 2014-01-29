using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KPart = global::Part;

namespace SeleneKSP.DataTypes
{
    class EngineInfo : Part, Selene.DataTypes.IEngineInfo
    {
        ModuleEngines engine;

        public EngineInfo(KPart newPart, ModuleEngines newEngine) : base(newPart)
        {
            engine = newEngine;
        }

        public bool GetEnabled()
        {
            return engine.getIgnitionState;
        }

        public void SetEnabled(bool newValue)
        {
            if(newValue)
            {
                engine.Activate();
            } else
            {
                engine.Shutdown();
            }
        }

        public double GetThrustPercentage()
        {
            return engine.thrustPercentage;
        }

        public void SetThrustPercentage(double newValue)
        {
            engine.thrustPercentage = (float)newValue;
        }

        public double GetMaxThrust()
        {
            return (double)engine.maxThrust;
        }
    }
}
