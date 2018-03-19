namespace Mesh
{
    using System.Collections.Generic;
    using System.Linq;

    using UnityEngine;

    [CreateAssetMenu] public class MeshAsset : ScriptableObject
    {
        [SerializeField] private MeshAsset[] meshAssets;
        [SerializeField] private TriangleMesh[] triangles;

        public Mesh ToMesh()
        {
            var effectiveTriangles = new List<TriangleMesh>(triangles);
            effectiveTriangles.AddRange(meshAssets.SelectMany(mesh => mesh.triangles));
            return TriangleMesh.ToUnityMesh(triangles);
        }
    }
}
