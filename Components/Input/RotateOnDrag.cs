using UnityEngine;

[RequireComponent(typeof(Draggable))]
public class RotateOnDrag : GameBehavior {

    Draggable draggable;

    Direction faceDirection;

    new Rigidbody rigidbody;
    [SerializeField]
    float stability;
    [SerializeField]
    float speed;

    protected override void InitComponents()
    {
        draggable = GetComponent<Draggable>();
        rigidbody = GetComponent<Rigidbody>();
    }

    protected sealed override void AddEventHandlers()
    {
        draggable.OnDrag += OnDragMotion;
        draggable.OnDragEnd += OnDragDeselect;
    }

    protected sealed override void RemoveEventHandlers()
    {
        draggable.OnDrag -= OnDragMotion;
        draggable.OnDragEnd -= OnDragDeselect;
    }

    void OnDragMotion(Draggable draggable, Drag drag, RaycastHit hitInfo) {
        Direction potentialFaceDirection = hitInfo.normal.DominantDirection();
        faceDirection = potentialFaceDirection == Direction.None ? faceDirection : potentialFaceDirection;
        Vector3 rawDragVector = drag.LastGesture.MousePositionCurrent - drag.LastGesture.MousePositionLast;
        Diagnostics.Log("Drag vector is " + rawDragVector, LogType.Dragging);
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

       
        Diagnostics.Log("Predicted up is " + predictedUpRight);
        Vector3 upTorque = Vector3.Cross(predictedUpRight, (Vector3.up + Vector3.right));
        Vector3 downTorque = Vector3.Cross(predictedUpRight, (Vector3.down + Vector3.right));

        Vector3 torqueVector = upTorque.magnitude < downTorque.magnitude ? upTorque : downTorque;
        rigidbody.AddTorque(torqueVector * speed * speed);
    }

    void OnDragDeselect(Draggable draggable, AbstractGesture drag)
    {        

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
