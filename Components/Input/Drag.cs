using UnityEngine;
using System.Collections;

public class Drag {

    public float? ElapsedTime
    {
        get;
        set;
    }

    public Vector3 MousePositionStart
    {
        get
        {
            return mousePositionStart;
        }
    }

    public Vector3 mousePositionStart;


    public Vector3? MousePositionLast
    {
        get;
        set;
    }

    public Vector3 MousePositionCurrent
    {
        get;
        set;
    }

    public Vector3 Velocity
    {
        get
        {
            Diagnostics.Log("current and start are " + MousePositionCurrent + " , " + MousePositionStart);
            return ElapsedTime.HasValue ? (MousePositionCurrent - MousePositionStart) / ElapsedTime.Value : Vector3.zero;
        }
    }

    public Drag(Vector3 mousePositionStart)
    {
        this.mousePositionStart = mousePositionStart;
    }	
}
