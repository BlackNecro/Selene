//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

namespace Selene.DataTypes
{
    public class SeleneQuaternion
    {
        private UnityEngine.Quaternion unityQuat;

        public SeleneQuaternion(UnityEngine.Quaternion init)
        {
            unityQuat = init;
        }
        public SeleneQuaternion(UnityEngine.QuaternionD init)
        {
            unityQuat = init;
        }
        public SeleneQuaternion(SeleneQuaternion init)
        {
            unityQuat = init.unityQuat;
        }
        public SeleneQuaternion(double x, double y, double z, double w)
        {
            unityQuat.x = (float)x;
            unityQuat.y = (float)y;
            unityQuat.z = (float)z;
            unityQuat.w = (float)w;
        }

        public UnityEngine.Quaternion Quaternion
        {
            set { unityQuat = value; }
            get { return unityQuat; }
        }
        public UnityEngine.QuaternionD QuaternionD
        {
            set { unityQuat = value; }
            get { return unityQuat; }
        }
       /* public SeleneVector eulerAngles
        {
            set { unityQuat.eulerAngles = value.Vector3D; }
            get { return new SeleneVector(unityQuat.eulerAngles); }
        }*/
        public double this[int index]
        {
            set { unityQuat[index] = (float)value; }
            get { return unityQuat[index]; }
        }
        public double x
        {
            set { unityQuat.x = (float)value; }
            get { return unityQuat.x; }
        }
        public double y
        {
            set { unityQuat.y = (float)value; }
            get { return unityQuat.y; }
        }
        public double z
        {
            set { unityQuat.z = (float)value; }
            get { return unityQuat.z; }
        }
        public double w
        {
            set { unityQuat.w = (float)value; }
            get { return unityQuat.w; }
        }

        public SeleneQuaternion Inversed
        {
            get
            {
                return Inverse(this);
            }
        }

        public override string ToString()
        {
            return "(" + unityQuat.x + ", " + unityQuat.y + ", " + unityQuat.z + ", " + unityQuat.w + ")";
        }

        public void SetEulerAngles(SeleneVector ea)
        {
            UnityEngine.Vector3 v = ea.Vector3D;
            unityQuat.eulerAngles = v;
        }
        public void SetEulerAngles(double x, double y, double z)
        {
            unityQuat.eulerAngles = new UnityEngine.Vector3( (float)x, (float)y, (float)z );
        }
        public SeleneVector GetEulerAngles()
        {
            return new SeleneVector(unityQuat.eulerAngles);
        }

        public static SeleneQuaternion operator *(SeleneQuaternion lhs, SeleneQuaternion rhs)
        {
            return lhs.unityQuat * rhs.unityQuat;
        }
        public static SeleneVector operator *(SeleneQuaternion rotation, SeleneVector point)
        {
            return rotation.QuaternionD * point.Vector3D;
        }
        /*public static bool operator !=(SeleneQuaternion lhs, SeleneQuaternion rhs)
        {
            return lhs.unityQuat != rhs.unityQuat;
        }
        public static bool operator ==(SeleneQuaternion lhs, SeleneQuaternion rhs)
        {
            return lhs.unityQuat == rhs.unityQuat;
        }
        public override int GetHashCode()
        {
            return unityQuat.GetHashCode();
        }
        public override bool Equals(object other)
        {

        }*/

        public static implicit operator SeleneQuaternion(UnityEngine.QuaternionD q)
        {
            return new SeleneQuaternion(q);
        }
        public static implicit operator UnityEngine.QuaternionD(SeleneQuaternion q)
        {
            return q.QuaternionD;
        }
        public static implicit operator SeleneQuaternion(UnityEngine.Quaternion q)
        {
            return new SeleneQuaternion(q);
        }
        public static implicit operator UnityEngine.Quaternion(SeleneQuaternion q)
        {
            return q.Quaternion;
        }

        public static SeleneQuaternion AngleAxis(double angle, SeleneVector axis)
        {
            return UnityEngine.Quaternion.AngleAxis((float)angle, axis.Vector3D);
        }
        public static double Dot(SeleneQuaternion a, SeleneQuaternion b)
        {
            return UnityEngine.Quaternion.Dot(a.unityQuat, b.unityQuat);
        }
        public static SeleneQuaternion FromToRotation(SeleneVector fromDirection, SeleneVector toDirection)
        {
            return UnityEngine.Quaternion.FromToRotation(fromDirection.Vector3D, toDirection.Vector3D);
        }
        public static SeleneQuaternion Inverse(SeleneQuaternion q)
        {
            return UnityEngine.Quaternion.Inverse(q.unityQuat);
        }
        public static SeleneQuaternion LookRotation(SeleneVector forward)
        {
            return UnityEngine.Quaternion.LookRotation(forward.Vector3D);
        }
        public static SeleneQuaternion LookRotation(SeleneVector forward, SeleneVector upwards)
        {
            return UnityEngine.Quaternion.LookRotation(forward.Vector3D, upwards.Vector3D);
        }
        public static SeleneQuaternion RotateTowards(SeleneQuaternion from, SeleneQuaternion to, double maxDegreesDelta)
        {
            return UnityEngine.Quaternion.RotateTowards(from.unityQuat, to.unityQuat, (float)maxDegreesDelta);
        }
        public static SeleneQuaternion Lerp(SeleneQuaternion from, SeleneQuaternion to, double t)
        {
            return UnityEngine.Quaternion.Lerp(from.unityQuat, to.unityQuat, (float)t);
        }
        public static SeleneQuaternion Slerp(SeleneQuaternion from, SeleneQuaternion to, double t)
        {
            return UnityEngine.Quaternion.Slerp(from.unityQuat, to.unityQuat, (float)t);
        }
    }
}
