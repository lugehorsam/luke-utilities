namespace Mesh
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using UnityEngine;

    public class CompositeMesh : IProceduralMesh
    {
        public List<IProceduralMesh> Meshes { get; } = new List<IProceduralMesh>();

        public ReadOnlyCollection<TriangleMesh> TriangleMeshes
        {
            get { return new ReadOnlyCollection<TriangleMesh>(Meshes.SelectMany(mesh => mesh.TriangleMeshes).ToList()); }
        }

        public Mesh ToUnityMesh()
        {
            return TriangleMesh.ToUnityMesh(Meshes.SelectMany(mesh => mesh.TriangleMeshes));
        }

        public void AddMesh(IProceduralMesh mesh)
        {
            Meshes.Add(mesh);
        }
    }
}
