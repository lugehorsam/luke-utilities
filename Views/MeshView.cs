using UnityEngine;

namespace Utilities.Meshes
{
	public class MeshView<T> : View where T : IProceduralMesh
	{
		public MeshRenderer MeshRenderer => _meshRenderer;
		private readonly MeshRenderer _meshRenderer;

		public MeshFilter MeshFilter => _meshFilter;
		private readonly MeshFilter _meshFilter;

		public MeshBinding MeshBinding => _meshBinding;
		private readonly MeshBinding _meshBinding;

		public T ProceduralMesh => _proceduralMesh;
		private readonly T _proceduralMesh;
		
		public MeshView(T proceduralMesh)
		{
			_meshRenderer = AddComponent<MeshRenderer>();
			_meshFilter = AddComponent<MeshFilter>();
			_meshBinding = new MeshBinding(proceduralMesh, _meshFilter);
			_proceduralMesh = proceduralMesh;
			_meshFilter.mesh = _proceduralMesh.ToUnityMesh();
		}
	}	
}
