using UnityEngine;
using System.Collections;

public class Motion : AbstractMotion {

    public sealed override Vector3 MousePositionStart
    {
        get;
        set;
    }

    public sealed override Vector3 MousePositionCurrent
    {
        get;
        set;
    }

    public sealed override Vector3? MousePositionLast
    {
        get;
        set;
    }

    public sealed override float? ElapsedTime
    {
        get;
        set;
    }

    public Motion(Vector3 mousePositionStart)
    {
        this.MousePositionStart = mousePositionStart;
    }
}
