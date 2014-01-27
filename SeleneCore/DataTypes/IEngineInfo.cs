using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Selene.DataTypes
{
    public interface IEngineInfo
    {
        Vector GetOffset();
        Boolean GetEnabled();
        void SetEnabled(bool newValue);
        double GetThrottlePercentage();
        void SetThrottlePercentage(double newValue);
        double GetMaxThrust();
    }
}
