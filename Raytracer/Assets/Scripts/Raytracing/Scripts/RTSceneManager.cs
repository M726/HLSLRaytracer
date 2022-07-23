using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using M726Raytracing;
using System;

namespace M726Raytracing {
    public class RTSceneManager : MonoBehaviour {
        private RTObject[] raytracingObjects;
        // Start is called before the first frame update
        void Start() {
            raytracingObjects = FindObjectsOfType(typeof(RTObject)) as RTObject[];
        }

        public ShaderObject[] GetShaderObjects() {
            ShaderObject[] objects = new ShaderObject[raytracingObjects.Length];
            for (int i = 0; i < raytracingObjects.Length; i++) {
                objects[i] = raytracingObjects[i].GetShaderObject();
            }
            return objects;
        }

        public RTObject CreateObject() {
            RTObject obj = new RTObject(ObjectType.Sphere);
            Array.Resize(ref raytracingObjects, raytracingObjects.Length + 1);
            raytracingObjects[raytracingObjects.Length - 1] = obj;
            return obj;
        }
    }
}
