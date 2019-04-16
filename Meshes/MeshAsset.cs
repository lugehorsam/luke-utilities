namespace Mesh
{
	using System.Collections.Generic;
	using System.Linq;

	using UnityEngine;

	[CreateAssetMenu]
	public class MeshAsset : ScriptableObject
	{
		[SerializeField] private MeshAsset[] meshAssets = new MeshAsset[] { };
		[SerializeField] private TriangleMesh[] triangles = new TriangleMesh[] { };

		public Mesh ToMesh()
		{
			var effectiveTriangles = new List<TriangleMesh>(triangles);
			effectiveTriangles.AddRange(meshAssets.SelectMany(mesh => mesh.triangles));
			return TriangleMesh.ToUnityMesh(triangles);
		}
	}
}
