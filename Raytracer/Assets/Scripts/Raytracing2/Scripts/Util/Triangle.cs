namespace M726Raytracing2 {
    public struct Triangle {
        public Vector3 v1, v2, v3;

        public Triangle(Vector3 v1, Vector3 v2, Vector3 v3) {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public override string ToString() {
            return v1.ToString() + v2.ToString() + v3.ToString();
        }
    }
}
