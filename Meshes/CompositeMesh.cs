using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Utilities.Meshes
{
	public class CompositeMesh : IMesh {

		public ReadOnlyCollection<TriangleMesh> TriangleMeshes
		{
			get { return new ReadOnlyCollection<TriangleMesh>(Meshes.SelectMany(mesh => mesh.TriangleMeshes).ToList()); }
		}

		public List<IMesh> Meshes
		{
			get { return _meshes; }
		}
		
		private readonly List<IMesh> _meshes = new List<IMesh>();

		public void AddMesh(IMesh mesh)
		{
			_meshes.Add(mesh);
		}

		public Mesh ToUnityMesh()
		{
			return TriangleMesh.ToUnityMesh(Meshes.SelectMany(mesh => mesh.TriangleMeshes));
		}
	}
}
