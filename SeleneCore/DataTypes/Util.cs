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
        public static Vector3d MultiplyQuat(UnityEngine.QuaternionD quat, Vector3d toMul)
        {
            return quat * toMul;
        }
        public static Vector3d MultiplyQuat(Vector3d toMul, UnityEngine.QuaternionD quat)
        {           
            return quat * toMul;
        }

        public static Vector3d AddVec(Vector3d a, Vector3d b)
        {
            return a + b;
        }
        public static Vector3d SubstractVec(Vector3d a, Vector3d b)
        {
            return a - b;
        }
        public static Vector3d MultiplyVec(Vector3d a, double b)
        {
            return a * b;
        }
        public static Vector3d DivideVec(Vector3d a, double b)
        {
            return a / b;
        }
    }
}
