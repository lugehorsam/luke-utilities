namespace Luke
{	
	using UnityEngine;
	using Utilities.Bindings;
	
	[CreateAssetMenu]
	public class SpriteSizeObject : Vector3Object
	{
		[SerializeField] private bool _useCameraWidth;
		[SerializeField] private float _cameraWidthRatio;

		public override BindType BindType => BindType.SpriteSize;

		protected override Vector3 ProcessProperty(Vector3 property)
		{
			var baseProperty = base.ProcessProperty(property);
			
			if (_useCameraWidth)
				baseProperty.x = Camera.main.aspect * Camera.main.orthographicSize * 2 * _cameraWidthRatio;
			
			return baseProperty;
		}
	}
}
