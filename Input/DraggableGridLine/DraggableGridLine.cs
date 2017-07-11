using System;
using System.Collections.Generic;

using UnityEngine;


namespace Utilities.Input
{
	public class DraggableGridLine<T> : View where T : View, IDraggableGridLineTile
	{
		private readonly LineBinding _lineBinding;
		
		public int? StartIndex
		{
			get { return _sourceItem == null ? null : new int?(_sourceItem.Index); }
		} 
		
		private T _sourceItem;
		private readonly GridLayout<T> _gridLayout;
		private readonly float _defaultZ;

		public DraggableGridLine(LineBinding lineBinding, GridLayout<T> gridLayout, float defaultZ)
		{
			_gridLayout = gridLayout;
			_defaultZ = defaultZ;
			_lineBinding = lineBinding;
			
			foreach (var touchDispatcher in gridLayout.GetTouchDispatchers())
			{
				touchDispatcher.OnTouch += HandleOnTouch;
				touchDispatcher.OnHold += HandleOnHold;
				touchDispatcher.OnRelease += HandleOnRelease;
			}
		}

		void HandleOnTouch(TouchDispatcher touchDispatcher, Gesture gesture)
		{
			T item = FromTouchDispatcher(touchDispatcher);
			_sourceItem = item;
		}

		T FromTouchDispatcher(TouchDispatcher dispatcher)
		{
			return dispatcher.GetComponent<ViewBinding>().View as T;
		}

		void HandleOnHold(TouchDispatcher touchDispatcher, Gesture gesture)
		{
			T item = FromTouchDispatcher(touchDispatcher);

			if (item == null)
			{
				Diagnostics.Report(new InvalidCastException());
				return;
			}
			
			if (item.IsInputWithinConnectBounds(UnityEngine.Input.mousePosition))
			{
				_lineBinding.SetPropertyPermanent(item.RectTransform.position.SetZ(_defaultZ));					
			}
			else
			{
				Vector3 dragWorldPoint = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
				
				var originalTile = _gridLayout[StartIndex.Value];
				Vector3 origCenter = originalTile.RectTransform.position;
			
				Diagnostics.Log("orig tile " + originalTile);
			
				Vector3 dragOffsetFromTile = origCenter - dragWorldPoint;                
				Axis dominantAxisFromSource = dragOffsetFromTile.DominantAxis(excludedAxes: new List<Axis>{Axis.Z});

				Vector3 targetPos;

				if (dominantAxisFromSource == Axis.X)
				{
					targetPos = new Vector3(dragWorldPoint.x, origCenter.y, _defaultZ);
				}
				else
				{
					targetPos = new Vector3(origCenter.x, dragWorldPoint.y, _defaultZ);   
				}
											
				Diagnostics.Log("target pos " + targetPos);
				_lineBinding.SetProperty(targetPos);
			}
		
		}

		void HandleOnRelease(TouchDispatcher touchDispatcher, Gesture gesture)
		{
			Diagnostics.Log("Clear");
			_lineBinding.Clear();
		}
	}
}

/**

			IEnumerable<T> edges = RuneLevel.RuneNodeSegments;
			RuneTileView heldTileView = dispatcher.GetComponent<ViewBinding>().View as RuneTileView;
			int tileIndex = TileLayout.Data.IndexOf(heldTileView);
			RuneNode existingNode = RuneLevel.RuneNodes[tileIndex];

			bool nodeAlreadyExists = existingNode == null;
			bool edgeAlreadyExists = edges.Any(edge => edge.Contains(tileIndex) && edge.Contains(_draggableEdge.CurrentIndex.Value));
			bool isCompleted = heldTileView.IsInputWithinConnectBounds();

			if (nodeAlreadyExists || edgeAlreadyExists)
				return;

			if (isCompleted && _draggableEdge != null)
			{
				RuneLevel.RuneNodeSegments.Add
				(
					new RuneEdge(existingNode.Index, heldTileView.Index, RuneLevel.RuneNodes)
				);

				_draggableEdge = null;                
				RuneLevel.RuneNodes[heldTileView.Index].Type.State = RuneNodeType.Filled;            
			}
			else
			{
				if (!_draggableEdge.CurrentIndex.HasValue)
				{
					_draggableEdge = new RuneDraggableEdge(heldTileView.Index, NodeGridLayout);
					_draggableEdge.Data = new RuneEdge(_CurrentIndex.Value, RuneLevel.CurrentNodeIndex, RuneLevel.RuneNodes);
				}

				var originalTile = TileLayout[_draggableEdge.Data.Start.Value];
                
				Vector3 dragWorldPoint = Camera.main.ScreenToWorldPoint(gesture.MousePositionCurrent);
				dragWorldPoint.z = RuneLayers.SEGMENT_VIEW;
				Vector3 origCenter = originalTile.RectTransform.position;
                
                
				Vector3 dragOffsetFromTile = origCenter - dragWorldPoint;                
				Axis dominantAxisFromSource = dragOffsetFromTile.DominantAxis(excludedAxes: new List<Axis>{Axis.Z});

				Vector3 targetPos;

				if (dominantAxisFromSource == Axis.X)
				{
					targetPos = new Vector3(dragWorldPoint.x, origCenter.y, RuneLayers.SEGMENT_VIEW);
				}
				else
				{
					targetPos = new Vector3(origCenter.x, dragWorldPoint.y, RuneLayers.SEGMENT_VIEW);   
				}
                             
				_draggableEdge.LineBinding.SetProperty(targetPos);                                  
			}
		}
**/