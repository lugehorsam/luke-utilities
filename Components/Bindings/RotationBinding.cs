using UnityEngine;
using System.Collections;
using System;

public class RotationBinding : Vector3Binding<Transform> {

    public override void SetProperty(Vector3 rot) {
        transform.localRotation = Quaternion.Euler(rot);
    }

    public override Vector3 GetProperty() {
        return transform.localRotation.eulerAngles;
    }
}
