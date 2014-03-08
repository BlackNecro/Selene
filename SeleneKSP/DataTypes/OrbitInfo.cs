using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;
using KOrbit = global::Orbit;

namespace SeleneKSP.DataTypes
{
    class OrbitInfo : IOrbitInfo
    {
        protected KOrbit orbit;
        protected KSPDataProvider dataProvider;

        public OrbitInfo(KSPDataProvider provider, KOrbit toUse)
        {
            dataProvider = provider;
            orbit = toUse;                       
        }

        public override string ToString()
        {
            return "( Ap:" + orbit.ApA + " Pe:" + orbit.PeA + " Inc:" + orbit.inclination + " Ecc:" + orbit.eccentricity + " )";
        }

        public ICelestialBody body
        {
            get
            {
                if (orbit.referenceBody != null)
                    return new CelestialBody(dataProvider, orbit.referenceBody);
                else
                    return null;
            }
        }

        public double ap { get { return orbit.ApA; } }
        public double apA { get { return orbit.ApA; } }
        public double apR { get { return orbit.ApR; } }

        public double pe { get { return orbit.PeA; } }
        public double peA { get { return orbit.PeA; } }
        public double peR { get { return orbit.PeR; } }

        public double semiLatusRectum { get { return orbit.semiLatusRectum; } }
        public double semiMinorAxis { get { return orbit.semiMinorAxis; } }

        public double eccentricity { get { return orbit.eccentricity; } }
        public double inclination { get { return orbit.inclination; } }
        public double period { get { return orbit.period; } }
        public double timeToAp { get { return orbit.timeToAp; } }
        public double timeToPe { get { return orbit.timeToPe; } }
    }
}
