using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public static class QuaternionUtil
    {
        public static UnityEngine.Quaternion Multiply(UnityEngine.Quaternion left, UnityEngine.Quaternion right)
        {
            return (left * right);
        }
        public static Vector Multiply(UnityEngine.Quaternion quat, Vector toMul)
        {
            return new Vector(quat * toMul.Vector3D);
        }
        public static Vector Multiply(Vector toMul, UnityEngine.Quaternion quat)
        {
            return new Vector(quat * toMul.Vector3D);
        }
    }
}
