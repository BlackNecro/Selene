using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Selene.DataTypes
{
    public interface IEngineInfo : IPart
    {
        Boolean GetEnabled();
        void SetEnabled(bool newValue);
        double GetThrustPercentage();
        void SetThrustPercentage(double newValue);
        double GetMaxThrust();
    }
}
