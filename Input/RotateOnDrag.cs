using UnityEngine;
using System.Linq;

namespace Utilities.Input
{ 
    [RequireComponent(typeof(TouchDispatcher))]
    public class RotateOnDrag : MonoBehaviour
    {
        Direction faceDirection;

        TouchDispatcher touchDispatcher;
        new Collider collider;
        new Rigidbody rigidbody;

        [SerializeField]
        bool debugMode;

        [SerializeField]
        float stability;
        [SerializeField]
        float speed;

        void Awake()
        {
            touchDispatcher = GetComponent<TouchDispatcher>();
            rigidbody = GetComponent<Rigidbody>();
            collider = GetComponent<Collider>();
            touchDispatcher.OnDrag += OnDrag;
            touchDispatcher.OnRelease += OnDragDeselect;
        }

        void OnDestroy()
        {
            touchDispatcher.OnDrag -= OnDrag;
            touchDispatcher.OnRelease -= OnDragDeselect;
        }

        void OnDrag(TouchDispatcher draggable, Gesture drag)
        {
            if (drag.GestureFrames[0].HitForCollider(collider) == null)
            {
                return;
            }

            GestureFrame lastFrame = drag.GestureFrames.Last();
            RaycastHit? hitForCollider = lastFrame.HitForCollider(collider);

            Direction potentialFaceDirection = hitForCollider.HasValue ? hitForCollider.Value.normal.DominantDirection() : Direction.None;
            faceDirection = potentialFaceDirection == Direction.None ? faceDirection : potentialFaceDirection;
            Vector3 rawDragVector = drag.MousePositionCurrent - drag.MousePositionLast.Value;
            Vector3 rawRotationVector = SwipeToTorqueVector(rawDragVector, faceDirection);
            rigidbody.AddTorque(rawRotationVector * speed);
        }

        void FixedUpdate()
        {
            //Stabilize();   
        }

        void Stabilize()
        {
            Vector3 predictedUpRight = Quaternion.AngleAxis(
                                           rigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * stability / speed,
                                           rigidbody.angularVelocity
                                       ) * (transform.up + transform.right);



            Vector3 upTorque = Vector3.Cross(predictedUpRight, (Vector3.up + Vector3.right));
            Vector3 downTorque = Vector3.Cross(predictedUpRight, (Vector3.down + Vector3.right));

            Vector3 torqueVector = upTorque.magnitude < downTorque.magnitude ? upTorque : downTorque;
            rigidbody.AddTorque(torqueVector * speed * speed);
        }

        void OnDragDeselect(TouchDispatcher draggable, Gesture drag)
        {
            if (debugMode)
            {
                Diagnostics.DrawGesture(drag);
            }
        }

        Vector3 SwipeToTorqueVector(Vector3 rawDragVector, Direction faceDirection)
        {
            switch (faceDirection)
            {
                case Direction.Forward:
                case Direction.Back:
                    return new Vector3(rawDragVector.y, -rawDragVector.x, 0f);
                case Direction.Up:
                    return new Vector3(rawDragVector.y, 0f, -rawDragVector.x);
                case Direction.Down:
                    return new Vector3(rawDragVector.y, 0f, rawDragVector.x);
                case Direction.Left:
                    return new Vector3(0f, -rawDragVector.x, -rawDragVector.y);
                case Direction.Right:
                    return new Vector3(0f, -rawDragVector.x, rawDragVector.y);
            }
            return Vector3.zero;
        }
    }
}
