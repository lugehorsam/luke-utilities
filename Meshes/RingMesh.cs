using System.Collections.Generic;

namespace Utilities.Meshes
{
	public class RingMesh : SimpleMesh
	{		
		public RingMesh(float originRadius, float radius)
		{
			List<Vertex> innerVertices = CircleMesh.CreateVertexRing(originRadius);
			List<Vertex> outerVertices = CircleMesh.CreateVertexRing(radius);
			
			for (int i = 0; i < CircleMesh._NUM_VERTEX_ITERATIONS; i++)
			{

				int modulatedIteration = (i + 1) % CircleMesh._NUM_VERTEX_ITERATIONS;
				var triangle1 = new TriangleMesh
				(
					outerVertices[i],
					innerVertices[modulatedIteration],
					innerVertices[i]
				);
				
				var triangle2 = new TriangleMesh
				(
					outerVertices[i],
					outerVertices[modulatedIteration],
					innerVertices[modulatedIteration]
				);
							
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

