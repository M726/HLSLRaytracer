using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracingController : MonoBehaviour {


    public ComputeShader raytracingShader;
    public RenderTexture sourceTexture;

    [Header("Camera")]
    [Range(2, 64)]
    public int samples = 16;
    [Range(0f, 179f)]
    public float fov = 90f;
    [Range(0f, 1000f)]
    public float drawDistance = 20f;
    [Range(0f, 10f)]
    public float clippingDistance = 1f;

    [Header("Spheres")]
    public int numSpheres = 5;
    public bool sphereGameObjectsBool = false;

    private struct ObjectPlane {
        public Vector3 normal;
        public Vector3 position;
        public PBRMaterial mat;
    };

     private struct ObjectSphere {
        public Vector3 position;
        public float radius;
        public PBRMaterial mat;
    };

    private struct LightDirectional {
        public Vector3 position;
        public Vector3 direction;
        public Vector3 color;
        public float intensity;
    };

    private struct LightPoint {
        public Vector3 position;
        public Vector3 color;
        public float intensity;
    };



    [Header("Debug")]
    public Vector3 lightPosition = Vector3.zero;
    public bool debugGizmos = false;
    public bool debugSampleReset = true;
    public int sampleResetNumber = 20000;
    [SerializeField]
    private uint _currentSample = 0;

    private Camera _camera;

    private Material _addMaterial;
    private Vector2 _pixelOffset = new Vector2(0.5f, 0.5f);

    ComputeBuffer sphereBuffer;
    ComputeBuffer pointLightBuffer;
    ComputeBuffer uvBuffer;

    private ObjectSphere[] _Spheres = { };
    private ObjectPlane[] _Planes = { };
    private LightPoint[] _LightPoints = { };
    

    private void SetShaderScene() {
        /*
        PBRMaterial mat;
        mat.diffuseColor = new Vector3(1f, 0f, 0f);
        mat.emissivity = 0f;
        mat.refractionIndex = 1.1f;
        mat.diffuse = 1.1f;
        mat.refractionIndex = 1.1f;
        _Spheres = new ObjectSphere[1];
        _Spheres[0] = new ObjectSphere() {
            position = new Vector3(0f, 0f, 0f),
            radius = 3.0f,
            mat = mat
        };

        sphereBuffer = new ComputeBuffer(_Spheres.Length, sizeof(float) * 9); //Stride of 7 floats for 3position, 1radius, 3color
        sphereBuffer.SetData(_Spheres);
        raytracingShader.SetBuffer(0, "_ObjectSpheres", sphereBuffer);
        */

        _LightPoints = new LightPoint[1];
        _LightPoints[0] = new LightPoint() {
            position = lightPosition,
            intensity = 1000f,
            color = new Vector3(1f, 0.8f, 0.8f),
        };

        pointLightBuffer = new ComputeBuffer(_LightPoints.Length, sizeof(float) * 7); //Stride of 7 floats for 3position, 1radius, 3color
        pointLightBuffer.SetData(_LightPoints);
        raytracingShader.SetBuffer(0, "_LightPoint", pointLightBuffer);
    }
    private void PostRender() {

        sphereBuffer.Release();
        pointLightBuffer.Release();
        uvBuffer.Release();
    }

    private void Update() {
        _camera = GetComponent<Camera>();

        if (transform.hasChanged) {
            ResetFrame();
        }
        if (debugSampleReset && _currentSample > sampleResetNumber) {
            ResetFrame();
        }
        //CreateScene();
    }

    private void ResetFrame() {
        _currentSample = 0;
        transform.hasChanged = false;
    }
    private void SetUVBuffer(double a, double b) {
        uvBuffer = new ComputeBuffer(2, sizeof(double) * 2);
        uvBuffer.SetData(new[] { a, b });
        raytracingShader.SetBuffer(0, "_PixelOffset", uvBuffer);
    }
    private void SetShaderParameters() {
        raytracingShader.SetMatrix("_CameraToWorld", _camera.cameraToWorldMatrix);
        raytracingShader.SetMatrix("_CameraInverseProjection", _camera.projectionMatrix.inverse);
        raytracingShader.SetVector("_ClippingPlanes", new Vector4(clippingDistance, drawDistance));
        raytracingShader.SetFloat("_RandomSeed", UnityEngine.Random.value * _currentSample);
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

    private void OnRenderImage(RenderTexture source, RenderTexture destination) {

        System.Random rand = new System.Random();
        SetUVBuffer(rand.NextDouble(), rand.NextDouble());

        SetShaderParameters();
        SetShaderScene();
        CreateSourceTexture();
        Render(destination);
        uvBuffer.Release();
        PostRender();
    }

    private void CreateScene() {
        GameObject[] gameObjects = (GameObject[])FindObjectsOfType(typeof(GameObject));

        string[] meshNames = new string[5] { "Cube", "Sphere", "Capsule", "Cylinder", "Plane" };

        foreach (GameObject obj in gameObjects) {
            if (obj.TryGetComponent(out MeshFilter filter)) {
                string name = filter.sharedMesh.name;
                foreach (string primName in meshNames) {
                    if (primName == name) {
                        switch (primName) {
                            case "Sphere":

                                break;
                            case "Plane":

                                break;
                        }
                    }
                }
            }
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


    private void OnDrawGizmos() {
        if (debugGizmos) {
            float a = Mathf.Tan(fov * Mathf.PI / 360f);
            float b = 2f / (samples - 1);
            for (int i = 0; i < samples; i++) {
                for (int j = 0; j < samples; j++) {
                    Vector3 direction = (transform.rotation * new Vector3(
                            a * (b * i - 1),
                            a * (b * j - 1),
                            1)).normalized;

                    foreach (ObjectSphere sphere in _Spheres) {
                        if (SphereLineIntersectionTest(sphere, transform.position, direction)) {
                            Gizmos.DrawRay(transform.position,
                                drawDistance * direction);
                        }
                    }
                }
            }
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.DrawFrustum(Vector3.zero, fov, drawDistance, clippingDistance, 1f);
        }
    }

    private Vector3 RandomVector3(float min, float max) {
        return new Vector3(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
    }

    private bool SphereLineIntersectionTest(ObjectSphere sphere, Vector3 lineOrigin, Vector3 lineDirection) {
        //https://en.wikipedia.org/wiki/Line%E2%80%93sphere_intersection
        if (lineDirection.magnitude > 1) lineDirection = lineDirection.normalized;
        Vector3 dPosition = lineOrigin - sphere.position;
        float uDotDPosition = Vector3.Dot(lineDirection, dPosition);
        float del = Mathf.Pow(uDotDPosition, 2) - (dPosition.sqrMagnitude - Mathf.Pow(sphere.radius, 2));

        if (del < 0) return false; //No Intersection

        float distanceFromLineOrigin = -uDotDPosition - Mathf.Sqrt(del);
        if (distanceFromLineOrigin - clippingDistance < 0 || distanceFromLineOrigin - drawDistance > 0) return false; //Sphere surface is between clipping planes
        return true;
    }

}
