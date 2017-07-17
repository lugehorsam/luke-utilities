using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Meshes
{
    public class MeshAsset : ScriptableObject {

        [SerializeField]
        private TriangleMesh[] triangles;

        [SerializeField]
        private MeshAsset[] meshAssets;

        public Mesh ToMesh()
        {
            List<TriangleMesh> effectiveTriangles = new List<TriangleMesh>(triangles);       
            effectiveTriangles.AddRange(meshAssets.SelectMany(mesh => mesh.triangles));            
            return TriangleMesh.ToUnityMesh(triangles);
        }
    }    
}
