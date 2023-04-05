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


        [SerializeField]
        [Curve(390f, 0f, 440f, 1f, true)]
        public AnimationCurve diffuseColor = new AnimationCurve(new[] { new Keyframe(390, 0.25f), new Keyframe(830, 0.25f) });

        [Range(0f, 1f)]
        public float roughness = 1f;

        [SerializeField]
        [Curve(390f, 0f, 440f, 100f, true)]
        public AnimationCurve emissive = new AnimationCurve(new[] { new Keyframe(390, 0f), new Keyframe(830, 0f) });

        //[SerializeField]
        //[Curve(390f, 0f, 440f, 1f, true)]
        //public AnimationCurve specularColor = new AnimationCurve(new[] { new Keyframe(390, 0f), new Keyframe(830, 0) });



        public MaterialStruct GetMaterialProperties(float wavelength) {
            return new MaterialStruct() {
                color = diffuseColor.Evaluate(wavelength),
                roughness = this.roughness,
                emissive = this.emissive.Evaluate(wavelength),
            };
        }
    }
    public struct MaterialStruct {
        public float color;
        public float roughness;
        public float emissive;
    }
}