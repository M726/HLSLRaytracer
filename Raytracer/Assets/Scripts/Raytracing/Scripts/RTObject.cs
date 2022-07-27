using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace M726Raytracing {
    [ExecuteInEditMode]
    public class RTObject : MonoBehaviour {

        private int id;
        public ObjectType type;
        private Vector3 position;
        private Vector3 localScale;
        private Vector4 rotation;

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

            rotation = transform.rotation.eulerAngles * Mathf.Deg2Rad;

        }

        public ShaderObject GetShaderObject() {
            return new ShaderObject {
                id = id,
                type = (int)type,
                position = position,
                scale = localScale,
                rotation = rotation,
                material = material.GetMaterialProperties()
            };
        }

        private void OnDrawGizmos() {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            switch (type) {
                case ObjectType.Sphere: {
                    Gizmos.DrawWireSphere(Vector3.zero, 1f);
                    break;
                }
                case ObjectType.Plane: {
                    Gizmos.DrawLine(new Vector3(-0.5f, 0, -0.5f), new Vector3(0.5f, 0, -0.5f));
                    Gizmos.DrawLine(new Vector3(-0.5f, 0, -0.5f), new Vector3(-0.5f, 0, 0.5f));
                    Gizmos.DrawLine(new Vector3(0.5f, 0, 0.5f), new Vector3(-0.5f, 0, 0.5f));
                    Gizmos.DrawLine(new Vector3(0.5f, 0, 0.5f), new Vector3(0.5f, 0, -0.5f));
                    break;
                }
                case ObjectType.Cube: {
                    Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
                    break;
                }
            }
        }

        public void SetID(int id) {
            this.id = id;
        }
    }
}