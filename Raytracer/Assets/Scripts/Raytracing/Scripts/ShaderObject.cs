using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M726Raytracing{
    public struct ShaderObject {
        public int id;
        public int type;
        public Vector3 position, scale;
        public Vector3 rotation;
        public MaterialProperties material;
    }

    public struct MaterialProperties {
        public Vector3 diffuseColor, specularColor, emissiveColor, transmissionColor;
        public float diffuse, specular, emissive, opacity;
        public float refraction;
    }

    public enum ObjectType {
        Sphere,
        Plane,
        Cube,
    }
}
