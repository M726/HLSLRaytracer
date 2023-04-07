using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace M726Raytracing2 {
    public class RaytracingController : MonoBehaviour {


        [Header("Raytracing Settings")]
        public bool run = true;
        public bool debug = false;
        public bool averageSamples = true;
        public uint currentSample = 0;

        public ComputeShader raytracingShader;
        public RenderTexture sourceTexture;
        private RenderTexture lastTexture;
        private Material addMaterial;

        List<Triangle> triangles;
        List<MaterialStruct> materials;

        [Header("Camera Settings")]
        public float exposure = 1f;

        [Header("Wavelength Settings")]
        public int wavelengthMin = 390;
        public int wavelengthMax = 830;

        public int wavelength = 532;
        private bool wavelengthRunning = false;

        public bool cycleWavelength = false;


        public Mesh[] meshes;

        Camera _camera;
        CSV humanSpectralSensitivity;

        ComputeBuffer triangleBuffer;
        ComputeBuffer materialBuffer;
        private bool bufferSet = false;

        private readonly System.Random rand = new System.Random();

        private void Reset() {
            Start();
        }

        private void Start() {
            meshes = GenerateMeshes();
            humanSpectralSensitivity = new CSV(@"Assets/Scripts/Raytracing2/Data/humanSpecSens.csv");
            _camera = Camera.main;
        }

        private void Update() {
            meshes = GenerateMeshes();
            if (transform.hasChanged || !averageSamples) {
                ResetFrame();
                transform.hasChanged = false;
                if (cycleWavelength) {
                    ResetWavelength();
                }
            }
        }

        private void ResetWavelength() {
            wavelength = wavelengthMin;
            ResetFrame();
        }
        private void ResetFrame() {
            currentSample = 0;
        }

        private Mesh[] GenerateMeshes() {
            GameObject[] gameObjects = FindObjectsOfType<GameObject>();
            List<Mesh> meshList = new List<Mesh>();
            foreach(GameObject gameObject in gameObjects) {
                if(gameObject.TryGetComponent(out RaytracingMeshRenderer meshRenderer)) {
                    meshList.Add(meshRenderer.GetMesh());
                }
            }
            return meshList.ToArray();
        }
        private void SetRenderProperties() {
            raytracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
            raytracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
            raytracingShader.SetFloat(Shader.PropertyToID("_Wavelength"), wavelength);
            raytracingShader.SetFloat(Shader.PropertyToID("_RandX"), (float)rand.NextDouble() - 0.5f);
            raytracingShader.SetFloat(Shader.PropertyToID("_RandY"), (float)rand.NextDouble() - 0.5f);

            float[] test = humanSpectralSensitivity.Interpolate(wavelength);
            UnityEngine.Vector3 hssColor = new UnityEngine.Vector3(test[0], test[1], test[2]);
            //if (!cycleWavelength) hssColor = new UnityEngine.Vector3(1, 1, 1);
            raytracingShader.SetVector(Shader.PropertyToID("_HSSRGB"), hssColor);
            raytracingShader.SetFloat(Shader.PropertyToID("_Exposure"), exposure);

        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination) {
            if (!run) return;

            CreateSourceTexture();
            SetRenderProperties();

            if (averageSamples) {
                if (!bufferSet) {
                    SetBuffers();
                }
                Render(destination);
            } else {
                SetBuffers();
                Render(destination);
                ReleaseBuffers();
            }
            

        }

        private void SetBuffers() {
            triangles = new List<Triangle>();
            materials = new List<MaterialStruct>();
            
            foreach (Mesh mesh in meshes) {
                triangles.AddRange(mesh.GetTriangles());
                materials.AddRange(Enumerable.Repeat(mesh.GetMaterial(wavelength), mesh.tris));
            }

            triangleBuffer = new ComputeBuffer(triangles.Count, sizeof(float) * 3 * 3);
            materialBuffer = new ComputeBuffer(materials.Count, sizeof(float) * 12);

            //Set buffer Data
            triangleBuffer.SetData(triangles);
            materialBuffer.SetData(materials);

            raytracingShader.SetBuffer(0, Shader.PropertyToID("TriangleBuffer"), triangleBuffer);
            raytracingShader.SetBuffer(0, Shader.PropertyToID("MaterialBuffer"), materialBuffer);

            bufferSet = true;
        }

        private void ReleaseBuffers() {
            triangleBuffer.Release();
            materialBuffer.Release();
            bufferSet = false;
        }

        private void CreateSourceTexture() {
            if (sourceTexture == null || sourceTexture.width != Screen.width || sourceTexture.height != Screen.height) {
                if (sourceTexture != null) sourceTexture.Release();

                sourceTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
                sourceTexture.enableRandomWrite = true;
                sourceTexture.Create();
            }
        }


        private void Render(RenderTexture destination) {
            raytracingShader.SetTexture(0, "Result", sourceTexture);

            
            raytracingShader.Dispatch(0, Mathf.CeilToInt(Screen.width / 8.0f), Mathf.CeilToInt(Screen.height / 8.0f), 1);


            if (addMaterial == null) {
                addMaterial = new Material(Shader.Find("Hidden/AverageFrames"));
            }
            addMaterial.SetFloat("_Sample", currentSample);

            Graphics.Blit(sourceTexture, lastTexture, addMaterial);
            Graphics.Blit(lastTexture, destination);

            if (cycleWavelength) {
                wavelength++;
                if (wavelength >= wavelengthMax) {
                    //run = false;
                    lastTexture = null;
                    ResetWavelength(); 
                }
            }
            currentSample++;
        }
    }
}
