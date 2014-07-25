using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Selene.DataTypes;

namespace Selene.Util
{
    public static class Math
    {
        public static double Clamp(double val, double vMin, double vMax)
        {
            return (val < vMin ? vMin : (val < vMax ? val : vMax));
        }
        public static Vector3d Clamp(Vector3d v, double vMin, double vMax)
        {
            return new Vector3d(Clamp(v.x, vMin, vMax), Clamp(v.y, vMin, vMax), Clamp(v.z, vMin, vMax));
        }
    }
}
