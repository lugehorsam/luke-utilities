using UnityEngine;
using System.Collections;

public abstract class AbstractGesture {

    public abstract float ElapsedTime
    {
        get;
    }

    public abstract Vector3 MousePositionStart
    {
        get;
    }

    public abstract Vector3 MousePositionLast
    {
        get;
    }

    public abstract Vector3 MousePositionCurrent
    {
        get;
    }

    public Vector3 Velocity
    {
        get
        {
            return ElapsedTime > 0f ? (MousePositionCurrent - MousePositionStart) / ElapsedTime : Vector3.zero;
        }
    }
}
