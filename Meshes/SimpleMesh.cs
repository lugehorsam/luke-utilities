using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Meshes
{
    [Serializable]
    public class SimpleMesh : IMesh
    {
        public ReadOnlyCollection<TriangleMesh> TriangleMeshes => new ReadOnlyCollection<TriangleMesh>(_triangles);

        protected readonly List<TriangleMesh> _triangles = new List<TriangleMesh>();
               
        public Mesh ToUnityMesh()
        {
            return TriangleMesh.ToUnityMesh(_triangles);
        }
    }
}
