using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public struct SeleneVector
    {
        public double x;
        public double y;
        public double z;

        public SeleneVector(UnityEngine.Vector3 init)
        {
            x = init.x;
            y = init.y;
            z = init.z;
        }
        public SeleneVector(Vector3d init)
        {
            x = init.x;
            y = init.y;
            z = init.z;
        }

        public SeleneVector(double x, double y, double z)
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

        public static implicit operator Vector3d(SeleneVector vec)
        {
            return vec.Vector3D;
        }
        public static implicit operator SeleneVector(Vector3d vec)
        {
            return new SeleneVector(vec);
        }

        public static SeleneVector operator *(SeleneVector a, float b) { return new SeleneVector(a.x * b, a.y * b, a.z * b); }
        public static SeleneVector operator *(SeleneVector a, double b) { return new SeleneVector(a.x * b, a.y * b, a.z * b); }
        public static SeleneVector operator /(SeleneVector a, double b) { return new SeleneVector(a.x / b, a.y / b, a.z / b); }
        public static SeleneVector operator +(SeleneVector a, SeleneVector b) { return new SeleneVector(a.x + b.x, a.y + b.y, a.z + b.z); }
        public static SeleneVector operator -(SeleneVector a, SeleneVector b) { return new SeleneVector(a.x - b.x, a.y - b.y, a.z - b.z); } 

        public static SeleneVector Add(SeleneVector left, SeleneVector right)
        {
            return (left + right);
        }

        public static SeleneVector Substract(SeleneVector left, SeleneVector right)
        {
            return (left - right);
        }

        public static SeleneVector Multiply(double toMul, SeleneVector vec)
        {
            return (vec * toMul);
        }
        public static SeleneVector Multiply(SeleneVector vec, double toMul)
        {
            return (vec * toMul);
        }

        public static SeleneVector Divide(SeleneVector vec, double toDiv)
        {
            return (vec / toDiv);
        }
        public static SeleneVector Divide(double toDiv, SeleneVector vec)
        {
            return (vec / toDiv);
        }
     
        public SeleneVector Normalized
        {
            get
            {
                double invLen = 1/Length;
                return new SeleneVector(x * invLen, y * invLen, z * invLen);
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
                SeleneVector newLen = (Normalized * value);
                x = newLen.x;
                y = newLen.y;
                z = newLen.z;
            }
        }

        public double Dot(SeleneVector other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        public SeleneVector Cross(SeleneVector other)
        {
            return new SeleneVector(this.y * other.z - this.z * other.x, this.z * other.x - this.x * other.z, this.x * other.y - this.y * other.x);
        }


    }
}
