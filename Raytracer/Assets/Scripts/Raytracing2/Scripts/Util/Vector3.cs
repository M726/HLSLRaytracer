using System;

namespace M726Raytracing2 {
    public struct Vector3 {
        // The x, y, and z coordinates of the vector
        public float x, y, z;

        // Constructor that allows you to specify the x, y, and z coordinates of the vector
        public Vector3(float x, float y, float z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public Vector3(UnityEngine.Vector3 v) {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        // Method that calculates the magnitude (length) of the vector
        public float Magnitude() {
            return (float)Math.Sqrt(x * x + y * y + z * z);
        }

        // Override of the ToString method, which allows you to convert a Vector3 object to a string
        public override string ToString() {
            return $"({x}, {y}, {z})";
        }

        // Operator overload for the + operator that allows you to add two Vector3 objects together
        public static Vector3 operator +(Vector3 a, Vector3 b) {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        // Operator overload for the - operator that allows you to subtract one Vector3 object from another
        public static Vector3 operator -(Vector3 a, Vector3 b) {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        // Operator overload for the * operator that allows you to multiply a Vector3 object by a scalar value
        public static Vector3 operator *(Vector3 a, float b) {
            return new Vector3(a.x * b, a.y * b, a.z * b);
        }

        // Operator overload for the / operator that allows you to divide a Vector3 object by a scalar value
        public static Vector3 operator /(Vector3 a, float b) {
            return new Vector3(a.x / b, a.y / b, a.z / b);
        }

        // Method that calculates the dot product of two Vector3 objects
        public static float Dot(Vector3 a, Vector3 b) {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector3 Cross(Vector3 a, Vector3 b) {
            return new Vector3(
                a.y * b.z - a.z * b.y, 
                a.z * b.x - a.x * b.z, 
                a.x * b.y - a.y * b.x);
        }

        public static implicit operator Vector3(UnityEngine.Vector3 v) => new Vector3(v);
    }
}