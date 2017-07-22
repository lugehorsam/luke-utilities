using System.Collections.Generic;

namespace Utilities.Meshes
{
	public class RingMesh : SimpleMesh
	{		
		public RingMesh(float originRadius, float radius)
		{
			List<Vertex> innerVertices = CircleMesh.CreateVertexRing(originRadius);
			List<Vertex> outerVertices = CircleMesh.CreateVertexRing(radius);
			
			for (int i = 0; i < CircleMesh._NUM_VERTEX_ITERATIONS - 1; i++)
			{			
				
				var triangle1 = new TriangleMesh
				(
					outerVertices[i],
					innerVertices[i + 1],
					innerVertices[i]
				);
				
				Diagnostics.Log(triangle1.ToString());
				var triangle2 = new TriangleMesh
				(
					outerVertices[i],
					outerVertices[i + 1],
					innerVertices[i + 1]
				);
				
				Diagnostics.Log(innerVertices[i].ToString());
				
				_triangles.Add
				(
					triangle1	
				);
				
				_triangles.Add
				(
					triangle2
				);
			}
		}
	}
}

