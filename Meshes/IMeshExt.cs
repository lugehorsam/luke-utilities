using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Mesh
{
	public static class IMeshExt {

		public static Vertex GetNearestVertex(this IProceduralMesh thisMesh, Vector3 worldPoint)
		{
			return thisMesh.GetVertices().OrderBy(vertex => Vector3.Distance(vertex.AsVector3, worldPoint)).FirstOrDefault();
		}

		public static IEnumerable<Vertex> GetVertices(this IProceduralMesh thisMesh)
		{
			return thisMesh.TriangleMeshes.SelectMany(triangle => triangle.Vertices);
		}	
		
	}
}
