namespace Utilities.Bindings
{
	using UnityEngine;
	using Meshes;
	
	public class MeshComponent : Vector3Component<MeshFilter>
	{
		public override BindType BindType => BindType.Vertex;

		public Vertex VertexToLerp
		{
			get;
			set;
		}		

		public override void SetProperty(Vector3 property)
		{
			VertexToLerp.Set(property);
		}

		public override Vector3 GetProperty()
		{
			return VertexToLerp.AsVector3;
		}
	}
}
