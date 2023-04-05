using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace M726Raytracing2 {
    public class Mesh {
        public Triangle[] triangles;
        public PBRMaterial material;
        public int tris = 0;

        public Mesh(Triangle[] triangles, PBRMaterial material) {
            this.triangles = triangles;
            this.material = material;
            this.tris = triangles.Length;

        }

        public Triangle[] GetTriangles() {
            return triangles;
        }

        public MaterialStruct GetMaterial(float wavelength) {
            return material.GetMaterialProperties(wavelength);
        }
    }
}
