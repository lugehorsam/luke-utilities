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
