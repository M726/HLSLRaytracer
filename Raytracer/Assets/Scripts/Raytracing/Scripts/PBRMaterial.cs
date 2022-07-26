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

        [Header("Refractive Index")]
        [SerializeField]
        [Range(1f, 10f)]
        private float refractionIndex = 1.1f;
        [SerializeField]
        [Range(0f, 10f)]
        private float extinctionCoefficient = 0f;



        //Diffuse Color
        public Vector3 GetDiffuseColor() {
            return new Vector3(diffuseColorPicker.r, diffuseColorPicker.g, diffuseColorPicker.b);
        }
        public void SetDiffuseColor(Vector3 diffuseColor) {
            diffuseColorPicker = new Color(diffuseColor.x, diffuseColor.y, diffuseColor.z);
        }
        public void SetDiffuseColor(Color color) {
            SetDiffuseColor(new Vector3(color.r, color.g, color.b));
        }
        public void SetDiffuseColor(float r, float g, float b) {
            SetDiffuseColor(new Vector3(r, g, b));
        }

        //Specular Color
        public Vector3 GetSpedularColor() {
            return new Vector3(specularColorPicker.r, specularColorPicker.g, specularColorPicker.b);
        }
        public void SetSpecularColor(Vector3 specularColor) {
            specularColorPicker = new Color(specularColor.x, specularColor.y, specularColor.z);
        }
        public void SetSpecularColor(Color color) {
            SetSpecularColor(new Vector3(color.r, color.g, color.b));
        }
        public void SetSpecularColor(float r, float g, float b) {
            SetSpecularColor(new Vector3(r, g, b));
        }

        //Diffuse Fraction
        public float GetDiffuse() {
            return diffuse;
        }
        public void SetDiffuse(float a) {
            diffuse = a;
        }

        //Specular Fraction
        public float GetSpecular() {
            return specular;
        }
        public void SetSpecular(float a) {
            specular = a;
        }

        //refraction index
        public void SetRefractionIndex(float rI) {
            refractionIndex = rI;
        }
        public float GetRefractionIndex() {
            return refractionIndex;
        }
        public float GetExtinctionCoefficient() {
            return extinctionCoefficient;
        }
        public void SetExtinctionCoefficient(float eC) {
            extinctionCoefficient = eC;
        }

        public Vector3 GetEmissiveColor() {
            return new Vector3(emissiveColorPicker.r, emissiveColorPicker.g, emissiveColorPicker.b);
        }
        public void SetEmissiveColor(Vector3 emissiveColor) {
            emissiveColorPicker = new Color(emissiveColor.x, emissiveColor.y, emissiveColor.z);
        }
        public float GetEmissive() {
            return emissivityIntensity;
        }
        public void SetEmissive(float emissive) {
            this.emissivityIntensity = emissive;
        }

        public MaterialProperties GetMaterialProperties() {
            return new MaterialProperties {
                diffuseColor = GetDiffuseColor(),
                specularColor = GetSpedularColor(),
                emissiveColor = GetEmissiveColor(),
                diffuse = GetDiffuse(),
                specular = GetSpecular(),
                emissive = GetEmissive(),
                refraction = GetRefractionIndex(),
                extinction = GetExtinctionCoefficient()
            };
        }
    }
}