using System;

namespace M726Raytracing {
    public class RTSceneManager {
        private RTObject[] raytracingObjects;
        int objectIdCounter = 0;
        // Start is called before the first frame update
        public RTSceneManager() {
            raytracingObjects = UnityEngine.Object.FindObjectsOfType(typeof(RTObject)) as RTObject[];
            foreach (RTObject obj in raytracingObjects) {
                obj.SetID(objectIdCounter.ToString());
                objectIdCounter++;
            }
        }

        public ShaderObject[] GetShaderObjects() {
            ShaderObject[] objects = new ShaderObject[raytracingObjects.Length];
            for (int i = 0; i < raytracingObjects.Length; i++) {
                objects[i] = raytracingObjects[i].GetShaderObject();
            }
            return objects;
        }

        public RTObject CreateObject(ObjectType type) {
            RTObject obj = new RTObject(type);
            Array.Resize(ref raytracingObjects, raytracingObjects.Length + 1);
            raytracingObjects[raytracingObjects.Length - 1] = obj;
            return obj;
        }
    }
}
