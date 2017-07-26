using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities.Meshes
{
	public class CompositeMesh : IProceduralMesh {

		public ReadOnlyCollection<TriangleMesh> TriangleMeshes
		{
			get { return new ReadOnlyCollection<TriangleMesh>(Meshes.SelectMany(mesh => mesh.TriangleMeshes).ToList()); }
		}

		public List<IProceduralMesh> Meshes => _meshes;

		private readonly List<IProceduralMesh> _meshes = new List<IProceduralMesh>();

		public void AddMesh(IProceduralMesh mesh)
		{
			_meshes.Add(mesh);
		}

		public Mesh ToUnityMesh()
		{
			return TriangleMesh.ToUnityMesh(Meshes.SelectMany(mesh => mesh.TriangleMeshes));
		}
	}
}
