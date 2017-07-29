using UnityEngine;

namespace Utilities
{
	public class MeshExt 
	{
		
		/// <summary>
		/// Gets the bounds of the mesh, applying scale to the bounds.
		/// </summary>
		public static Vector3 GetScaledSize(Mesh mesh, Transform transform) 
		{	        
			Vector3 size = mesh.bounds.size;
			Vector3 scale = transform.localScale;
			size.Scale(scale);
			return size;
		}
	}	
}
