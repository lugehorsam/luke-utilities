using UnityEngine;

namespace Utilities.Meshes
{
	public class CircleMesh : SimpleMesh
	{
		private const int _ANGLE_PER_ITERATION = 1;
		
		public CircleMesh(float radius)
		{
			var lastPoint = new Vertex(0f, radius, 0f);
			var origin = new Vertex(0f, 0f, 0f);

			int numIterations = 360/_ANGLE_PER_ITERATION;
			
			for (int i = 0; i <= numIterations; i++)
			{
				Vertex point = new Vertex(Quaternion.AngleAxis(i * _ANGLE_PER_ITERATION, Vector3.back) * Vector3.up);

				var triangle = new TriangleMesh
				(
					new Vertex(lastPoint.X, lastPoint.Y, 0f),
					new Vertex(point.X, point.Y, 0f),
					origin
				);

				lastPoint = point;
				
				_triangles.Add(triangle);
			}
		}
	}
}

