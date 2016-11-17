using UnityEngine;
using System.Collections;

public abstract class AbstractMotion {

    public abstract float? ElapsedTime
    {
        get;
        set;
    }

    public abstract Vector3 MousePositionStart
    {
        get;
        set;
    }

    public abstract Vector3? MousePositionLast
    {
        get;
        set;
    }

    public abstract Vector3 MousePositionCurrent
    {
        get;
        set;
    }

    public Vector3 Velocity
    {
        get
        {
            return ElapsedTime.HasValue ? (MousePositionCurrent - MousePositionStart) / ElapsedTime.Value : Vector3.zero;
        }
    }
}
