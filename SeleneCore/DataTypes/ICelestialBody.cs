using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLua;

namespace Selene.DataTypes
{
    public interface ICelestialBody : ITarget
    {
        double radius { get; }
        double mass { get; }
        double gravity { get; }
        double atmosphereThickness { get; }
        double rotationPeriod { get; }
        double sphereOfInfluence { get; }

        IOrbitInfo orbit { get; }
        ICelestialBody referenceBody { get; }

        LuaTable GetOrbitingBodies();
        double GetLat(SeleneVector wpos);
        double GetLon(SeleneVector wpos);
        SeleneVector GetLatLonPos(double lat, double lon);
    }
}
