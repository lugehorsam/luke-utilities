namespace TouchDispatch
{
	using System.ComponentModel;

	using UnityEngine;

	using Utilities;

	public sealed class TouchDispatch : MonoBehaviour
	{
		public delegate void TouchHandler(TouchEventInfo info);

		[SerializeField] private Dimension _dimension = default(Dimension);

		public TouchLogic TouchLogic { get; } = new TouchLogic();

		public event TouchHandler OnDown = delegate { };
		public event TouchHandler OnRelease = delegate { };
		public event TouchHandler OnDrag = delegate { };
		public event TouchHandler OnDownOff = delegate { };
		public event TouchHandler OnProcess = delegate { };

		private void Update()
		{
			if (!ValidateDimension())
			{
				return;
			}

			Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			UpdateTouchLogic(mouseWorldPoint);
			TouchEventInfo touchInfo = CreateTouchInfo(mouseWorldPoint);
			DispatchEvents(touchInfo);
			OnProcess(touchInfo);
		}

		private bool IsMouseOver(Vector3 mouseWorldPoint)
		{
			switch (_dimension)
			{
				case Dimension.Two:
					return mouseWorldPoint.IsOver(GetComponent<Collider2D>());
				case Dimension.Three:
					return mouseWorldPoint.IsOver(GetComponent<Collider>());
			}

			return false;
		}

		private bool ValidateDimension()
		{
			if (_dimension == Dimension.None)
			{
				throw new InvalidEnumArgumentException("Must have a dimension.");
			}

			return true;
		}

		private void UpdateTouchLogic(Vector3 mouseWorldPoint)
		{
			bool isFirstDown = Input.GetMouseButtonDown(0);
			bool isDown = Input.GetMouseButton(0);
			bool isOver = IsMouseOver(mouseWorldPoint);
			bool isRelease = Input.GetMouseButtonUp(0);

			mouseWorldPoint.z = transform.position.z;

			TouchLogic.UpdateFrame(mouseWorldPoint, isDown, !isFirstDown, isRelease, isOver);
		}

		private void DispatchEvents(TouchEventInfo touchInfo)
		{
			if (TouchLogic.IsFirstDown)
			{
				OnDown(touchInfo);
			}

			if (TouchLogic.IsFirstDownOff)
			{
				OnDownOff(touchInfo);
			}

			if (TouchLogic.IsDrag)
			{
				OnDrag(touchInfo);
			}

			if (TouchLogic.IsRelease)
			{
				OnRelease(touchInfo);
			}
		}

		private TouchEventInfo CreateTouchInfo(Vector3 mouseWorldPoint)
		{
			TouchEventInfo info;

			if (_dimension == Dimension.Two)
			{
				RaycastHit2D[] hits = Physics2D.RaycastAll(mouseWorldPoint, Vector2.zero, 1000f);
				info = new TouchEventInfo(this, TouchLogic, hits, mouseWorldPoint);
			}
			else
			{
				RaycastHit[] hits = Physics.RaycastAll(mouseWorldPoint, Vector3.forward, 1000f);
				info = new TouchEventInfo(this, TouchLogic, hits, mouseWorldPoint);
			}

			return info;
		}
	}
}
