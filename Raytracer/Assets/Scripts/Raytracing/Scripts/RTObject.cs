using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace M726Raytracing {
    [ExecuteInEditMode]
    public class RTObject : MonoBehaviour {

        public ObjectType type;
        private Vector3 position;
        private Vector3 localScale;
        private Vector4 rotation;

        public PBRMaterial material;

        private string id;
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

        public void SetID(string id) {
            this.id = id;
        }

        public string GetID() {
            if(!string.IsNullOrEmpty(id)) 
                return id;

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[32];
            System.Random random = new System.Random();

            for (int i = 0; i < stringChars.Length; i++) {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}