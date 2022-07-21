using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New PBRMaterial", menuName = "PBRMaterial", order = 1)]
[System.Serializable]
public class PBRMaterial : ScriptableObject {
    public enum PBRMaterialType {
        nonmetal,
        metal
    }

    public PBRMaterialType materialType;

    [Header("Diffuse")]
    [SerializeField]
    private Color diffuseColorPicker = Color.white;
    [SerializeField]
    [Range(0f, 1f)]
    private float diffuse = 1.0f;
    //[SerializeField]
    [Range(0f, 1f)]
    private float diffuseRoughness = 1.0f;


    [Header("Specular")]
    [SerializeField]
    private Color specularColorPicker = Color.white;
    [SerializeField]
    [Range(0f, 1f)]
    private float specular = 1.0f;
    //[SerializeField]
    [Range(0f, 1f)]
    private float specularRoughness = 1.0f;



    [SerializeField]
    [Range(0.1f, 10f)]
    private float refractionIndex = 1.1f;

    [SerializeField]
    [Range(0f, 1f)]
    private float transmissionFraction = 0.0f;

    [Header("Emissivity")]
    [SerializeField]
    private Color emissiveColorPicker = Color.white;
    [SerializeField]
    [Range(0f, 10f)]
    private float emissivityIntensity = 0.0f;

    //Need to be vector3s because it's easier to send to float3 in compute shader
    private Vector3 diffuseColor = Vector3.one;
    private Vector3 specularColor = Vector3.one;



    //Diffuse Color
    public Vector3 GetDiffuseColor() {
        return new Vector3(diffuseColorPicker.r, diffuseColorPicker.g, diffuseColorPicker.b);
    }
    public void SetDiffuseColor(Vector3 diffuseColor) {
        this.diffuseColor = diffuseColor;
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
        return specularColor;
    }
    public void SetSpecularColor(Vector3 specularColor) {
        this.specularColor = diffuseColor;
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
    public float GetRefractionIndex() {
        // This can be changed either with 
        return 0f;
    }
    public void SetRefractionIndex() {
        if(materialType == PBRMaterialType.metal) {

        }
    }
    public void SetMaterialType(PBRMaterialType materialType) {
        this.materialType = materialType;
    }

    private void OnValidate() {

    }


}