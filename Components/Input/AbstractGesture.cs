using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public abstract class AbstractGesture {

    public ReadOnlyCollection<GestureFrame> GestureFrames
    {
        get
        {
            return new ReadOnlyCollection<GestureFrame>(gestureFrames);
        }
    }

    public abstract float ElapsedTime
    {
        get;
    }

    public abstract Vector3 MousePositionStart
    {
        get;
    }

    public abstract Vector3? MousePositionLast
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

    protected List<GestureFrame> gestureFrames;

    public void AddGestureFrame(GestureFrame frame)
    {
        gestureFrames.Add(frame);
    }
}
