using UnityEngine;

namespace M726Raytracing2 {
    public class RaytracingMeshRenderer:MonoBehaviour {
        public UnityEngine.Mesh mesh;
        public PBRMaterial material;

        public bool showMesh = false;

        private Mesh worldMesh;
         
        public void Start() {
            worldMesh = UpdateMesh();
        }

        public void Update() {
            if (transform.hasChanged) {
                worldMesh = UpdateMesh();
                transform.hasChanged = false;
            }
        }

        private Mesh UpdateMesh() {

            Triangle[] tris = new Triangle[mesh.triangles.Length / 3];
            UnityEngine.Vector3[] verts = mesh.vertices;
            Matrix4x4 ltwMatrix = transform.localToWorldMatrix;

            for (int i = 0; i < tris.Length; i++) {
                tris[i] = new Triangle(
                    ltwMatrix.MultiplyPoint3x4(verts[mesh.triangles[i * 3]]),
                    ltwMatrix.MultiplyPoint3x4(verts[mesh.triangles[i * 3 + 1]]),
                    ltwMatrix.MultiplyPoint3x4(verts[mesh.triangles[i * 3 + 2]])
                    );
            }
            
            return new Mesh(tris,material);
        }

        public Mesh GetMesh() {
            return worldMesh;
        }

        public void OnDrawGizmos() {
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.color = Color.green;
            Gizmos.DrawWireMesh(mesh);
            Gizmos.color = Color.white;
            if(showMesh)
            Gizmos.DrawMesh(mesh);
            
        }
    }
}
