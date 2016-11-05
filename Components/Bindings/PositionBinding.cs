using UnityEngine;
using System.Collections;
using System;

public class PositionBinding : Vector3Binding<Transform> {

    public PositionSpace PositionSpace {
        get {
            return positionSpace;
        }
        set {
            positionSpace = value;
        }
    }

    [SerializeField]
    PositionSpace positionSpace = PositionSpace.LocalPosition;

    public override void SetProperty(Vector3 position) {
        if (positionSpace == PositionSpace.LocalPosition) {
            transform.localPosition = position;
        } else {
            transform.position = position;
        }
    }
  
    public override Vector3 GetProperty() {
        if (positionSpace == PositionSpace.LocalPosition) {
            return transform.localPosition;
        } else {
            return transform.position;
        }
    }
}
