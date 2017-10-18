namespace Utilities.Bindings
{
	using UnityEngine;

	[CreateAssetMenu]
	public class PositionProperty : Vector3Property<Transform>
	{
		[SerializeField] private Vector2 _anchor;
		[SerializeField] private bool _useAnchor;
		[SerializeField] PositionSpace _positionSpace = PositionSpace.Local;

		protected override Vector3 ProcessInitialProperty(Vector3 property)
		{
			if (_useAnchor)
			{
				Vector3 worldAnchoredPosition = Camera.main.ViewportToWorldPoint
				(
					new Vector2(_anchor.x, _anchor.y)				
				);
			
				worldAnchoredPosition.z = property.z;
				
				property = worldAnchoredPosition;
			}

			return property;
		}
		
		protected sealed override void Set(Transform transform, Vector3 position) 
		{            
			if (_positionSpace == PositionSpace.Local) 
			{
				transform.localPosition = position;
			} 
			else 
			{
				transform.position = position;
			}
		}
  
		protected sealed override Vector3 Get(Transform transform) 
		{
			if (_positionSpace == PositionSpace.Local) 
			{
				return transform.localPosition;
			} 

			return transform.position;
		}
	}   
}
