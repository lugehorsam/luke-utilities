using UnityEngine;
using System.Collections.ObjectModel;

namespace Utilities.Meshes
{
	public interface IMesh {
		ReadOnlyCollection<TriangleMesh> TriangleMeshes { get; }
		Mesh ToUnityMesh();
	}	
}
