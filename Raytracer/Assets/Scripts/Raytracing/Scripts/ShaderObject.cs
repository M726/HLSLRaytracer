using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M726Raytracing{
    public struct ShaderObject {
        public int type;
        public Vector3 position, scale, rotation;
        public MaterialProperties material;
    }

    public struct MaterialProperties {
        public Vector3 diffuseColor, specularColor, emissiveColor;
        public float diffuse, specular, emissive;
        public float refraction, extinction;
    }

    public enum ObjectType {
        Sphere,
        Plane,
        Cube,
    }
}
