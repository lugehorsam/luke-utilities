namespace Utilities
{
	using UnityEngine;

	[ExecuteInEditMode]
	public class SortingLayerAssigner : MonoBehaviour
	{
		[SerializeField] private string _sortingLayerName;
		[SerializeField] private int _sortingLayerOrder;
		
		private void Awake()
		{
			AssignSortingLayer();
		}
		
		private void OnValidate()
		{
			AssignSortingLayer();
		}

		private void AssignSortingLayer()
		{
			var renderer = GetComponent<Renderer>();
			renderer.sortingLayerID = SortingLayer.NameToID(_sortingLayerName);
			renderer.sortingOrder = _sortingLayerOrder;
		}	
	}
}