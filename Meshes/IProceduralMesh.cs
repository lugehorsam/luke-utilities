using UnityEngine;
using System.Collections.ObjectModel;

namespace Utilities.Meshes
{
	public interface IProceduralMesh {
		ReadOnlyCollection<TriangleMesh> TriangleMeshes { get; }
		Mesh ToUnityMesh();
	}	
}
