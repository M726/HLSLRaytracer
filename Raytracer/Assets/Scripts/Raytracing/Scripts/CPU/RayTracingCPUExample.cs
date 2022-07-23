using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTracingCPUExample : MonoBehaviour {


    [Header("Camera")]
    [Range(2,64)]
    public int samples = 16;
    [Range(0f, 179f)]
    public float fov = 90f;
    [Range(0f, 100f)]
    public float drawDistance = 20f;
    [Range (0f,10f)]
    public float clippingDistance = 1f;


    [Header("Spheres")]
    public int numSpheres = 5;
    public bool sphereGameObjectsBool = false;

    public struct Sphere {
        public Vector3 Position;
        public float radius;
        public Vector3 color;
    }

    public Sphere[] spheres = { };
    public GameObject[] sphereGameObjects;


    private void Update() {
        CreateScene();
    }
    public void CreateScene() {
        spheres = GenerateRandomSpheres();
        if(sphereGameObjectsBool) GenerateRandomSphereGameObjects();
    }

    private Sphere[] GenerateRandomSpheres() {
        Sphere[] spheres = new Sphere[numSpheres];
        UnityEngine.Random.InitState(30);
        for (int i = 0; i < numSpheres; i++){
            spheres[i] = new Sphere { Position = RandomVector3(-10, 10), radius = UnityEngine.Random.Range(1f, 5f), color = RandomVector3(0f, 1f) };
        }
        return spheres;
    }

    private void GenerateRandomSphereGameObjects() {
        for (int i = 0; i < sphereGameObjects.Length; i++) {
            Destroy(sphereGameObjects[i]);
        }
        sphereGameObjects = new GameObject[numSpheres];
        for (int i = 0; i < sphereGameObjects.Length; i++) {
            sphereGameObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphereGameObjects[i].transform.position = spheres[i].Position;
            sphereGameObjects[i].transform.localScale = 2f * Vector3.one * spheres[i].radius;
        }
    }

    private void OnDrawGizmos() {
        float a = Mathf.Tan(fov * Mathf.PI / 360f);
        float b = 2f / (samples - 1);
        for (int i = 0; i < samples; i++) {
            for (int j = 0; j < samples; j++) {
                Vector3 direction = (transform.rotation * new Vector3(
                        a * (b * i - 1),
                        a * (b * j - 1),
                        1)).normalized;

                foreach (Sphere sphere in spheres) {
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

    private Vector3 RandomVector3(float min, float max) {
        return new Vector3(UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max), UnityEngine.Random.Range(min, max));
    }

    public bool SphereLineIntersectionTest(Sphere sphere, Vector3 lineOrigin, Vector3 lineDirection) {
        //https://en.wikipedia.org/wiki/Line%E2%80%93sphere_intersection
        if(lineDirection.magnitude > 1) lineDirection = lineDirection.normalized;
        Vector3 dPosition = lineOrigin - sphere.Position;
        float uDotDPosition = Vector3.Dot(lineDirection, dPosition);
        float del = Mathf.Pow(uDotDPosition,2)-(dPosition.sqrMagnitude - Mathf.Pow(sphere.radius,2));

        if (del < 0) return false; //No Intersection

        float distanceFromLineOrigin = -uDotDPosition - Mathf.Sqrt(del);
        if (distanceFromLineOrigin - clippingDistance < 0 || distanceFromLineOrigin - drawDistance > 0) return false; //Sphere surface is between clipping planes
        return true;
    }

}
