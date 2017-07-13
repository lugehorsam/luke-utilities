using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.Input
{
	public class DraggableGridLine<T> : View where T : View, IDraggableGridLineTile
	{		
		private readonly LineBinding _lineBinding;
				
		public int? CurrentIndex
		{
			get { return _currentTile == null ? null : new int?(_currentTile.Index); }
		}
		
		private T _currentTile;		
		
		private readonly GridLayout<T> _gridLayout;
		private readonly float _defaultZ;

		public DraggableGridLine(LineBinding lineBinding, GridLayout<T> gridLayout, float defaultZ)
		{
			_gridLayout = gridLayout;
			_defaultZ = defaultZ;
			_lineBinding = lineBinding;
			
			foreach (var touchDispatcher in gridLayout.GetTouchDispatchers())
			{
				touchDispatcher.OnFirstDown += HandleOnFirstDown;
				touchDispatcher.OnDrag += HandleOnDrag;
				touchDispatcher.OnRelease += HandleOnRelease;
			}
		}

		void HandleOnFirstDown(TouchEventInfo eventInfo)
		{			
			var view = eventInfo.TouchDispatcher.View;
			T tile = view as T;
			
			Diagnostics.Log("first down on " + view );
			Vector3 position = view.GameObject.GetComponent<RectTransform>().position;
			_lineBinding.Clear();
			_lineBinding.SetInitialProperty(position.SetZ(_defaultZ));
			_lineBinding.SetProperty(position.SetZ(_defaultZ));
			_currentTile = tile;
		}

	

		void HandleOnDrag(TouchEventInfo eventInfo)
		{
			TouchDispatcher dispatcher = eventInfo.TouchDispatcher;
			RaycastHit? hit = eventInfo.Hits.FirstOrDefault();
			
			T sourceItem = dispatcher.View as T;
			T hitItem = hit.HasValue ? hit.Value.collider.GetView() as T : null;

			Vector3 dragWorldPoint = Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
			
			if (hitItem != null && hitItem != sourceItem && hitItem.IsInputWithinConnectBounds(dragWorldPoint))
			{
				Diagnostics.Log("binding to " + hitItem);
				_lineBinding.SetPropertyPermanent(hitItem.RectTransform.position.SetZ(_defaultZ));
				_currentTile = hitItem;
			}
			else
			{
				
				var originalTile = _gridLayout[CurrentIndex.Value];
				Vector3 origCenter = originalTile.RectTransform.position;
						
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
											
				_lineBinding.SetProperty(targetPos.SetZ(_defaultZ));
			}
		}

		void HandleOnRelease(TouchEventInfo eventInfo)
		{
			_lineBinding.Clear();
			_currentTile = null;
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