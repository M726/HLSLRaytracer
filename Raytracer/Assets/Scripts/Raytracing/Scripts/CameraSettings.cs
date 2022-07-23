using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace M726Raytracing {
    [CreateAssetMenu(fileName = "New Camera Settings", menuName = "Raytracing/Camera Settings", order = 1)]
    [System.Serializable]
    public class CameraSettings : ScriptableObject {
        private float fov;
        private float nearPlane;
        private float farPlane;
        private float aspectRatio;
    }
}