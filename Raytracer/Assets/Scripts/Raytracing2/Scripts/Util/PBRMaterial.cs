using UnityEngine;
using UnityEditor;

namespace M726Raytracing2 {
    //[CreateAssetMenu(fileName = "New Material", menuName = "Raytracing2/Material", order = 1)]
    [System.Serializable]
    //public class Material : ScriptableObject {
    public class PBRMaterial {
        //[Range(0f, 1f)]
        //public float color = 0.0f;

        //[Range(0f, 10f)]
        //public float emissive = 0.0f;

        public float index = 1f;
        public float roughness = 1f;

        public Color colorDiffuse = Color.white;
        public Color colorSpecular = Color.white;
        public Color colorEmissive = Color.black;
        public float emissiveIntensity = 0f;

        public UnityEngine.Vector3 ColorToVector3(Color c) {
            return new UnityEngine.Vector3((float)c.r, (float)c.g, (float)c.b);
        }
        


        public MaterialStruct GetMaterialProperties(float wavelength) {
            return new MaterialStruct() {
                index = this.index,
                roughness = this.roughness,
                colorDiffuse = ColorToVector3(this.colorDiffuse),
                colorSpecular = ColorToVector3(this.colorSpecular),
                colorEmissive = ColorToVector3(this.colorEmissive),
                emissiveIntensity = this.emissiveIntensity,
            };
        }
    }
    public struct MaterialStruct {
        public float index;
        public float roughness;
        public UnityEngine.Vector3 colorDiffuse;
        public UnityEngine.Vector3 colorSpecular;
        public UnityEngine.Vector3 colorEmissive;
        public float emissiveIntensity;
    }
}