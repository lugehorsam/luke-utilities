namespace Utilities.Bindings
{
	using UnityEngine;
	
	public class SpritePivotBinding : Vector3Binding<SpriteRenderer> 
	{
		public override Vector3 GetProperty()
		{
			return _Component.sprite.pivot;
		}

		public override void SetProperty(Vector3 property)
		{
			var pivotedSprite = Sprite.Create(_Component.sprite.texture, _Component.sprite.rect, property);
			_Component.sprite = pivotedSprite;
		}
	}	
}
