using UnityEngine;
using System.Collections.Generic;

namespace Utilities.Meshes
{
	public interface IMesh {
		List<TriangleMesh> TriangleMeshes { get; }
		Mesh ToUnityMesh();
	}	
}
