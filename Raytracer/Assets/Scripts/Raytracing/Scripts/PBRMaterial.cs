using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace M726Raytracing {
    [CreateAssetMenu(fileName = "New PBRMaterial", menuName = "Raytracing/PBRMaterial", order = 1)]
    [System.Serializable]
    public class PBRMaterial : ScriptableObject {


        [Header("Diffuse")]
        [SerializeField]
        private Color diffuseColorPicker = Color.white;
        [SerializeField]
        [Range(0f, 1f)]
        private float diffuse = 1.0f;
        //[SerializeField]
        //[Range(0f, 1f)]
        //private float diffuseRoughness = 1.0f;


        [Header("Specular")]
        [SerializeField]
        private Color specularColorPicker = Color.white;
        [SerializeField]
        [Range(0f, 1f)]
        private float specular = 1.0f;
        //[SerializeField]
        //[Range(0f, 1f)]
        //private float specularRoughness = 1.0f;

        [Header("Emissivity")]
        [SerializeField]
        private Color emissiveColorPicker = Color.white;
        [SerializeField]
        [Range(0f, 10f)]
        private float emissivityIntensity = 0.0f;

        [Header("Transmission")]
        [SerializeField]
        private Color transmissiveColorPicker = Color.white;
        [SerializeField]
        [Range(0f, 1f)]
        private float opacity = 0.0f;

        [Header("Refractive Index")]
        [SerializeField]
        [Min(1)]
        private float refractionIndex = 1.1f;



        //Diffuse Color
        public Vector3 GetDiffuseColor() {
            return new Vector3(diffuseColorPicker.r, diffuseColorPicker.g, diffuseColorPicker.b);
        }

        //Specular Color
        public Vector3 GetSpedularColor() {
            return new Vector3(specularColorPicker.r, specularColorPicker.g, specularColorPicker.b);
        }

        //Diffuse Fraction
        public float GetDiffuse() {
            return diffuse;
        }

        //Specular Fraction
        public float GetSpecular() {
            return specular;
        }

        //refraction index
        public float GetRefractionIndex() {
            return refractionIndex;
        }

        public Vector3 GetEmissiveColor() {
            return new Vector3(emissiveColorPicker.r, emissiveColorPicker.g, emissiveColorPicker.b);
        }
        public float GetEmissive() {
            return emissivityIntensity;
        }

        public Vector3 GetTransmissiveColor() {
            return new Vector3(transmissiveColorPicker.r, transmissiveColorPicker.g, transmissiveColorPicker.b);
        }
        public float GetOpacity() {
            return opacity;
        }

        public MaterialProperties GetMaterialProperties() => new MaterialProperties {
            diffuseColor = GetDiffuseColor(),
            specularColor = GetSpedularColor(),
            emissiveColor = GetEmissiveColor(),
            transmissionColor = GetTransmissiveColor(),
            diffuse = GetDiffuse(),
            specular = GetSpecular(),
            emissive = GetEmissive(),
            opacity = GetOpacity(),
            refraction = GetRefractionIndex(),
        };
    }
}