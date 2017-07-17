using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Meshes
{
    [Serializable]
    public class ProceduralMesh : IMesh
    {
        public List<TriangleMesh> TriangleMeshes
        {
            get { return triangles; }
        }
        private readonly List<TriangleMesh> triangles = new List<TriangleMesh>();
               
        public Mesh ToUnityMesh()
        {
            return TriangleMesh.ToUnityMesh(triangles);
        }
    }
}
