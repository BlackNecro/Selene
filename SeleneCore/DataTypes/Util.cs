using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public static class VectorQuaternionUtil
    {
        public static UnityEngine.QuaternionD MultiplyQuat(UnityEngine.QuaternionD left, UnityEngine.QuaternionD right)
        {
            return (left * right);
        }
        public static SeleneVector MultiplyQuat(UnityEngine.QuaternionD quat, SeleneVector toMul)
        {
            return new SeleneVector(quat * toMul);
        }
        public static SeleneVector MultiplyQuat(SeleneVector toMul, UnityEngine.QuaternionD quat)
        {
            return new SeleneVector(quat * toMul);
        }

        public static SeleneVector AddVec(SeleneVector a, SeleneVector b)
        {
            return a + b;
        }
        public static SeleneVector SubstractVec(SeleneVector a, SeleneVector b)
        {
            return a - b;
        }
        public static SeleneVector MultiplyVec(SeleneVector a, double b)
        {
            return a * b;
        }
        public static SeleneVector DivideVec(SeleneVector a, double b)
        {
            return a / b;
        }
    }
}
