using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace M726Raytracing {
    [ExecuteInEditMode]
    public class RTObject : MonoBehaviour {

        public ObjectType type;
        private Vector3 position;
        private Vector3 localScale;
        private Vector3 rotation;

        public PBRMaterial material;

        private ObjectType lastObjectType;

        public RTObject(ObjectType objectType) {
            type = objectType;
        }

        private void Awake() {
            Update();
        }

        private void Update() {
            if (lastObjectType != type) {
                switch (type) {
                    case ObjectType.Sphere: {
                        transform.localScale = Vector3.one * (1f / Mathf.Sqrt(12));
                        break;
                    }
                    case ObjectType.Plane: {
                        transform.localScale = Vector3.one;
                        break;
                    }
                    case ObjectType.Cube: {
                        transform.localScale = Vector3.one;
                        break;
                    }
                }
                lastObjectType = type;
            }

            position = transform.position;
            localScale = transform.localScale;
            rotation = transform.rotation.eulerAngles;
        }

        public ShaderObject GetShaderObject() {
            return new ShaderObject {
                type = (int)type,
                position = position,
                scale = localScale,
                rotation = rotation,
                material = material.GetMaterialProperties()
            };
        }

        private void OnDrawGizmos() {
            switch (type) {
                case ObjectType.Sphere: {
                    Gizmos.DrawSphere(position, localScale.magnitude);
                    break;
                }
                case ObjectType.Plane: {
                    break;
                }
                case ObjectType.Cube: {
                    Gizmos.DrawCube(position, localScale);
                    break;
                }
            }
        }
    }
}