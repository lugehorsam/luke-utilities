using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Meshes
{
	public static class IMeshExt {

		public static void ModifyVertex(this IMesh thisMesh, Vertex oldVertex, Vertex newVertex)
		{
		}

		public static Vertex GetNearestVertex(this IMesh thisMesh, Vector3 worldPoint)
		{
			return thisMesh.GetVertices().OrderBy(vertex => Vector3.Distance(vertex.AsVector3, worldPoint)).FirstOrDefault();
		}

		public static IEnumerable<Vertex> GetVertices(this IMesh thisMesh)
		{
			return thisMesh.TriangleMeshes.SelectMany(triangle => triangle.Vertices);
		}	
		
	}
}
