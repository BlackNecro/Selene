using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public static class QuaternionUtil
    {
        public static UnityEngine.QuaternionD Multiply(UnityEngine.QuaternionD left, UnityEngine.QuaternionD right)
        {
            return (left * right);
        }
        public static Vector Multiply(UnityEngine.QuaternionD quat, Vector toMul)
        {
            return new Vector(quat * toMul.Vector3D);
        }
        public static Vector Multiply(Vector toMul, UnityEngine.QuaternionD quat)
        {
            return new Vector(quat * toMul.Vector3D);
        }
    }
}
