using UnityEngine;
using System.Collections;
using System;

public class PositionBinding : Vector3Binding<Transform> {

    public PositionBinding(MonoBehaviour coroutineRunner, GameObject gameObject) : base(coroutineRunner, gameObject)
    {
        
    }
    
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
            GameObject.transform.localPosition = position;
        } else {
            GameObject.transform.position = position;
        }
    }
  
    public override Vector3 GetProperty() {
        if (positionSpace == PositionSpace.LocalPosition) {
            return  GameObject.transform.localPosition;
        } else {
            return  GameObject.transform.position;
        }
    }
}
