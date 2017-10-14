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
			if (_Component.sprite == null)
			{
				Diag.Warn($"Trying to pivot a sprite renderer with no sprite on {gameObject}");
				return;
			}
			
			var pivotedSprite = Sprite.Create(_Component.sprite.texture, _Component.sprite.rect, property);
			_Component.sprite = pivotedSprite;
		}
	}	
}
