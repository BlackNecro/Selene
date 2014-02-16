using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;

namespace SeleneKSP.Util
{
    class Math
    {
        public static double Clamp(double val, double vMin, double vMax)
        {
            return (val < vMin ? vMin : (val < vMax ? val : vMax));
        }
        public static SeleneVector Clamp(SeleneVector v, double vMin, double vMax)
        {
            return new SeleneVector(Clamp(v.x, vMin, vMax), Clamp(v.y, vMin, vMax), Clamp(v.z, vMin, vMax));
        }
    }
}
