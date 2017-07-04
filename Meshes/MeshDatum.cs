using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    [Serializable]
    public struct MeshDatum
    {
        public Mesh Mesh
        {
            get { return useMeshAsset ? meshAsset.ToMesh() : TriangleDatum.ToUnityMesh(triangles); }
        }

        [SerializeField]
        private List<TriangleDatum> triangles;

        public Color Color
        {
            get { return color; }
        }

        [SerializeField]
        private Color color;

        [SerializeField] private MeshAsset meshAsset;
        [SerializeField] private bool useMeshAsset;

        public MeshDatum(List<TriangleDatum> triangles, Color color = default(Color), MeshAsset meshAsset = null, bool useMeshAsset = false)
        {
            this.triangles = triangles ?? new List<TriangleDatum>();
            this.color = color;
            this.meshAsset = meshAsset;
            this.useMeshAsset = useMeshAsset;
        }

        public void AddTriangle(TriangleDatum triangle)
        {
            triangles = triangles ?? new List<TriangleDatum>();
            triangles.Add(triangle);
        }
    }
}
