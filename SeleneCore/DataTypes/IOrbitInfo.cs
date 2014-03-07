using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IOrbitInfo
    {
        double ap { get; } // ApA
        double apA { get; } // surface
        double apR { get; } // center
        double pe { get; } // PeA
        double peA { get; } // surface
        double peR { get; } // center

        double semiLatusRectum { get; }
        double semiMinorAxis { get; }

        double eccentricity { get; }
        double inclination { get; }
        double period { get; }
        double timeToAp { get; }
        double timeToPe { get; }
    }
}
