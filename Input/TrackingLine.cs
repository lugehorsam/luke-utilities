using UnityEngine;
using Utilities.Input;

namespace Utilities
{
	public class TrackingLine : Controller
	{
		public LineBinding LineBinding => _lineBinding;

		private LineBinding _lineBinding;
		
		private TouchDispatcher<TrackingLine> _touchDispatcher;
		private Camera _camera;
		private float _z;
		
		public TrackingLine(Camera camera, TouchDispatcher<TrackingLine> dispatcher, float z)
		{
			_camera = camera;
			_lineBinding = new LineBinding(AddComponent<LineRenderer>());
			_z = z;
			_touchDispatcher = dispatcher;
			_touchDispatcher.OnDrag += HandleDrag;
			_touchDispatcher.OnRelease += HandleRelease;

		}

		void HandleDrag(TouchEventInfo<TrackingLine> eventInfo)
		{
			_lineBinding.SetProperty(_camera.ScreenToWorldPoint(UnityEngine.Input.mousePosition).SetZ(_z));
		}

		void HandleRelease(TouchEventInfo<TrackingLine> eventInfo)
		{
			_lineBinding.SetProperty(_lineBinding.GetProperty(0));
		}
	}
}
