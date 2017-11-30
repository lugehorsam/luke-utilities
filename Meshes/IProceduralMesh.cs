using UnityEngine;
using System.Collections.ObjectModel;

namespace Mesh
{
	using Mesh = UnityEngine.Mesh;

	public interface IProceduralMesh {
		ReadOnlyCollection<TriangleMesh> TriangleMeshes { get; }
		Mesh ToUnityMesh();
	}	
}
