using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public interface IManeuverNode
    {
        void SetTime( double ut );
        double GetTime();
        double time { get; }

        void SetTimeTo(double dt);
        double GetTimeTo();
        double timeTo { get; }

        void SetRadial(double v);
        double GetRadial();
        double radial { get; }

        void SetNormal(double v);
        double GetNormal();
        double normal { get; }

        void SetPrograde(double v);
        double GetPrograde();
        double prograde { get; }

        void SetDeltaV(Vector3d dv);
        Vector3d GetDeltaV();
        Vector3d deltaV { get; }

        Vector3d GetBurnVector();
        Vector3d burnVector { get; }

        IOrbitInfo GetResultingOrbit();
        IOrbitInfo resultingOrbit { get; }
    }
}
