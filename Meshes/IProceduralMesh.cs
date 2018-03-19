namespace Mesh
{
    using System.Collections.ObjectModel;

    using UnityEngine;

    public interface IProceduralMesh
    {
        ReadOnlyCollection<TriangleMesh> TriangleMeshes { get; }
        Mesh ToUnityMesh();
    }
}
