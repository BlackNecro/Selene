using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public class SeleneVectorToKill
    {
        public double x;
        public double y;
        public double z;

        public SeleneVectorToKill()
        {
            x = y = z = 0;
        }

        public SeleneVectorToKill(UnityEngine.Vector3 init)
        {
            x = init.x;
            y = init.y;
            z = init.z;
        }
        public SeleneVectorToKill(Vector3d init)
        {
            x = init.x;
            y = init.y;
            z = init.z;
        }

        public SeleneVectorToKill(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }


        public Vector3d Vector3D
        {
            set
            {
                x = value.x;
                y = value.y;
                z = value.z;
            }
            get
            {
                return new Vector3d(x, y, z);
            }
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ", " + z + ")";
        }

        public static implicit operator Vector3d(SeleneVectorToKill d)
        {
            return d.Vector3D;
        }

        public static SeleneVectorToKill operator *(SeleneVectorToKill a, float b) { return new SeleneVectorToKill(a.x * b, a.y * b, a.z * b); }
        public static SeleneVectorToKill operator *(SeleneVectorToKill a, double b) { return new SeleneVectorToKill(a.x * b, a.y * b, a.z * b); }
        public static SeleneVectorToKill operator /(SeleneVectorToKill a, double b) { return new SeleneVectorToKill(a.x / b, a.y / b, a.z / b); }
        public static SeleneVectorToKill operator +(SeleneVectorToKill a, SeleneVectorToKill b) { return new SeleneVectorToKill(a.x + b.x, a.y + b.y, a.z + b.z); }
        public static SeleneVectorToKill operator -(SeleneVectorToKill a, SeleneVectorToKill b) { return new SeleneVectorToKill(a.x - b.x, a.y - b.y, a.z - b.z); } 

        public static SeleneVectorToKill Add(SeleneVectorToKill left, SeleneVectorToKill right)
        {
            return (left + right);
        }

        public static SeleneVectorToKill Substract(SeleneVectorToKill left, SeleneVectorToKill right)
        {
            return (left - right);
        }

        public static SeleneVectorToKill Multiply(double toMul, SeleneVectorToKill vec)
        {
            return (vec * toMul);
        }
        public static SeleneVectorToKill Multiply(SeleneVectorToKill vec, double toMul)
        {
            return (vec * toMul);
        }

        public static SeleneVectorToKill Divide(SeleneVectorToKill vec, double toDiv)
        {
            return (vec / toDiv);
        }
        public static SeleneVectorToKill Divide(double toDiv, SeleneVectorToKill vec)
        {
            return (vec / toDiv);
        }
     
        public SeleneVectorToKill Normalized
        {
            get
            {
                double invLen = 1/Length;
                return new SeleneVectorToKill(x * invLen, y * invLen, z * invLen);
            }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(x * x + y * y + z * z);
            }
            set
            {
                SeleneVectorToKill newLen = (Normalized * value);
                x = newLen.x;
                y = newLen.y;
                z = newLen.z;
            }
        }

        public double Dot(SeleneVectorToKill other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        public SeleneVectorToKill Cross(SeleneVectorToKill other)
        {
            return new SeleneVectorToKill(this.y * other.z - this.z * other.x, this.z * other.x - this.x * other.z, this.x * other.y - this.y * other.x);
        }


    }
}
