using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace M726Raytracing {
    public class RTController : MonoBehaviour {

        public ComputeShader raytracingShader;
        public RenderTexture sourceTexture;

        private RTSceneManager sceneManager;

        [Header("Camera")]
        [Min(1)]
        public int samples = 16;
        [Range(0f, 179f)]
        public float fov = 90f;
        [Range(0f, 1000f)]
        public float drawDistance = 20f;
        [Range(0f, 10f)]
        public float clippingDistance = 1f;


        [Header("Debug")]

        public bool debugSampleReset = true;
        public int sampleResetNumber = 20000;
        [SerializeField]
        private uint _currentSample = 0;

        private Camera _camera;

        private Material _addMaterial;
        private Vector2 _pixelOffset = new Vector2(0.5f, 0.5f);

        private List<ComputeBuffer> computeBuffers = new List<ComputeBuffer>();
        private readonly System.Random rand = new System.Random();

        private void Start() {
            sceneManager = new RTSceneManager();
        }
        private void Awake() {
            _camera = GetComponent<Camera>();
        }

        private void Update() {
            if (transform.hasChanged || (debugSampleReset && _currentSample > sampleResetNumber)) {
                _currentSample = 0;
                transform.hasChanged = false;
            }
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination) {
            SetShaderScene();
            SetShaderParameters();
            CreateSourceTexture();
            Render(destination);
            ReleaseBuffers();
        }

        private void SetShaderScene() {
            sceneManager.UpdateSceneObjects();
            ShaderObject[] shaderObjects = sceneManager.GetShaderObjects();
            ComputeBuffer objectBuffer = new ComputeBuffer(shaderObjects.Length, sizeof(float) * 23 + sizeof(int) * 2);
            objectBuffer.SetData(shaderObjects);
            raytracingShader.SetBuffer(0, "_Objects", objectBuffer);
            computeBuffers.Add(objectBuffer);
        }

        private void SetShaderParameters() {
            ComputeBuffer uvBuffer = new ComputeBuffer(2, sizeof(double) * 2);
            uvBuffer.SetData(new[] { rand.NextDouble(), rand.NextDouble() });
            computeBuffers.Add(uvBuffer);

            raytracingShader.SetBuffer(0, "_PixelOffset", uvBuffer);
            raytracingShader.SetInt("_Rays", samples);
            raytracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
            raytracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
            raytracingShader.SetVector("_ClippingPlanes", new Vector4(clippingDistance, drawDistance));
            raytracingShader.SetFloat("_RandomSeed", UnityEngine.Random.value * _currentSample);
        }

        private void ReleaseBuffers() {
            foreach(ComputeBuffer computeBuffer in computeBuffers) {
                computeBuffer.Release();
            }
            computeBuffers.Clear();
        }
        private void CreateSourceTexture() {
            if (sourceTexture == null || sourceTexture.width != Screen.width || sourceTexture.height != Screen.height) {
                if (sourceTexture != null) sourceTexture.Release();

                sourceTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear);
                sourceTexture.enableRandomWrite = true;
                sourceTexture.Create();
                _currentSample = 0;
            }
        }



        private void Render(RenderTexture destination) {
            raytracingShader.SetTexture(0, "Result", sourceTexture);
            raytracingShader.Dispatch(0, Mathf.CeilToInt(Screen.width / 8.0f), Mathf.CeilToInt(Screen.height / 8.0f), 1);

            if (_addMaterial == null) {
                _addMaterial = new Material(Shader.Find("Hidden/RayTracingMultipleSamples"));
            }
            _addMaterial.SetFloat("_Sample", _currentSample);
            _currentSample++;
            Graphics.Blit(sourceTexture, destination, _addMaterial);
        }
    }
}