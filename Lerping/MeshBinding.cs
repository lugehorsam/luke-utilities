using UnityEngine;

namespace Utilities.Meshes
{
	
	public class MeshBinding : Vector3Binding<MeshFilter>
	{
		public Vertex VertexToLerp
		{
			get;
			set;
		}
		
		private readonly IMesh _proceduralMesh;
		private MeshFilter _meshFilter;
		
		public MeshBinding(IMesh proceduralMesh, GameObject gameObject, MeshFilter meshFilter) : base(gameObject, meshFilter)
		{
			_proceduralMesh = proceduralMesh;
			_meshFilter = meshFilter;
		}

		public override void SetProperty(Vector3 property)
		{
			VertexToLerp.Set(property);
			_meshFilter.mesh = _proceduralMesh.ToUnityMesh();
		}

		public override Vector3 GetProperty()
		{
			return VertexToLerp.AsVector3;
		}
	}
}
