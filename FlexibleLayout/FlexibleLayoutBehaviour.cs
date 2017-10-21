namespace Utilities
{
	using UnityEngine;
	
	public class FlexibleLayoutBehaviour : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer _spriteRenderer;
		public Vector2 Size => _spriteRenderer.size;
	}	
}
