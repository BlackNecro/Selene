using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selene.DataTypes
{
    public class Vector
    {
        public double x;
        public double y;
        public double z;

        public Vector()
        {
            x = y = z = 0;
        }
        public Vector(Vector3d init)
        {
            x = init.x;
            y = init.y;
            z = init.z;
        }

        public Vector(double x, double y, double z)
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

        public static implicit operator Vector3d(Vector d)
        {
            return d.Vector3D;
        }

        public static Vector operator *(Vector a, float b) { return new Vector(a.x * b, a.y * b, a.z * b); }
        public static Vector operator *(Vector a, double b) { return new Vector(a.x * b, a.y * b, a.z * b); }
        public static Vector operator /(Vector a, double b) { return new Vector(a.x / b, a.y / b, a.z / b); }
        public static Vector operator +(Vector a, Vector b) { return new Vector(a.x + b.x, a.y + b.y, a.z + b.z); }
        public static Vector operator -(Vector a, Vector b) { return new Vector(a.x - b.x, a.y - b.y, a.z - b.z); } 

        public static Vector Add(Vector left, Vector right)
        {
            return (left + right);
        }

        public static Vector Substract(Vector left, Vector right)
        {
            return (left - right);
        }

        public static Vector Multiply(double toMul, Vector vec)
        {
            return (vec * toMul);
        }
        public static Vector Multiply(Vector vec, double toMul)
        {
            return (vec * toMul);
        }

        public static Vector Divide(Vector vec, double toDiv)
        {
            return (vec / toDiv);
        }
        public static Vector Divide(double toDiv, Vector vec)
        {
            return (vec / toDiv);
        }
     
        public Vector Normalized
        {
            get
            {
                double invLen = 1/Length;
                return new Vector(x * invLen, y * invLen, z * invLen);
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
                Vector newLen = (Normalized * value);
                x = newLen.x;
                y = newLen.y;
                z = newLen.z;
            }
        }

        public double Dot(Vector other)
        {
            return x * other.x + y * other.y + z * other.z;
        }

        public Vector Cross(Vector other)
        {
            return new Vector(this.y * other.z - this.z * other.x, this.z * other.x - this.x * other.z, this.x * other.y - this.y * other.x);
        }


    }
}
