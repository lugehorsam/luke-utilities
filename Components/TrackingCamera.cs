using UnityEngine;
using System.Collections;
using DigitalRuby.Tween;

[RequireComponent(typeof(Camera))]

/// <summary>
/// TODO REWRITE UNDER NEW LERP CLASS
/// </summary>
public class TrackingCamera : MonoBehaviour {

    [SerializeField]
    float lerpTime;

    Camera cameraComponent;

    void Awake() {
        cameraComponent = GetComponent<Camera> ();
    }
        
    public void MoveToViewOnY(Transform viewObject, float desiredY) {

        Vector3 objectViewPortPoint = cameraComponent.WorldToScreenPoint (viewObject.transform.position);

        float newY = cameraComponent.GetWorldPositionForViewport (viewObject, new Vector3(objectViewPortPoint.x, desiredY, objectViewPortPoint.z)).y;
    }                    
}
