namespace Utilities.Bindings
{
	using UnityEngine;
	
	public class SpritePivotProperty : Vector3Property<SpriteRenderer> 
	{
		protected override Vector3 Get(SpriteRenderer component)
		{
			return component.sprite.pivot;
		}

		protected override void Set(SpriteRenderer component, Vector3 property)
		{
			if (component.sprite == null)
			{
				Diag.Warn($"Trying to pivot a sprite renderer with no sprite on {component.gameObject}");
				return;
			}
			
			var pivotedSprite = Sprite.Create(component.sprite.texture, component.sprite.rect, property);
			component.sprite = pivotedSprite;
		}
	}	
}
