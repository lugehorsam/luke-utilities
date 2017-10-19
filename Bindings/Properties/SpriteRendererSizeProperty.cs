namespace Utilities.Bindings
{
	using UnityEngine;

	[CreateAssetMenu]
	public class SpriteRendererSizeProperty : Property<Vector2, SpriteRenderer>
	{
		[SerializeField] private bool _useCameraWidth;
		[SerializeField] private float _cameraWidthRatio;
		
		protected override Vector2 Get(SpriteRenderer component)
		{
			return component.size;
		}

		protected override Vector2 ProcessInitialProperty(Vector2 property)
		{
			if (_useCameraWidth)
				property.x = Camera.main.aspect * Camera.main.orthographicSize * 2 * _cameraWidthRatio;
		
			return property;
		}

		protected override void Set(SpriteRenderer component, Vector2 size)
		{			
			component.size = size;		
		}
	}
} 
