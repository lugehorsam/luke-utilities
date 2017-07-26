using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Meshes
{
	public class CircleMesh : SimpleProceduralMesh
	{
		public const int _NUM_VERTEX_ITERATIONS =  _NUM_DEGREES/_ANGLE_PER_ITERATION;
		private const int _NUM_DEGREES = 360;
		private const int _ANGLE_PER_ITERATION = 1;
		
		public CircleMesh(float radius)
		{
			var lastVertex = new Vertex(0f, radius, 0f);
			var originVertex = new Vertex(0f, 0f, 0f);
			
			CreateVertexRing(radius, vertex =>
			{
				_triangles.Add
				(
					new TriangleMesh
					(
  					 originVertex,
					 lastVertex,
					 vertex					
					)					
				);

				lastVertex = vertex;
			});			
		}

		public static List<Vertex> CreateVertexRing(float radius, Action<Vertex> onVertexCreate = null)
		{
			var vertices = new List<Vertex>();
		
			for (int i = 0; i < _NUM_VERTEX_ITERATIONS; i++)
			{
				Vertex point = new Vertex(Quaternion.AngleAxis(i * _ANGLE_PER_ITERATION, Vector3.back) * Vector3.up * radius);
				vertices.Add(point);
				onVertexCreate?.Invoke(point);
			}

			return vertices;
		}
	}
}

