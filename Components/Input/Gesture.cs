using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;


public class Gesture : AbstractGesture {

    public sealed override Vector3 MousePositionStart
    {
        get
        {
            return FirstFrame.Position;
        }
    }

    public sealed override Vector3 MousePositionCurrent
    {
        get
        {
            return CurrentFrame.Position;
        }
    }

    public sealed override Vector3? MousePositionLast
    {
        get
        {
            return LastFrame.Position;
        }
    }

    public override float ElapsedTime
    {
        get
        {
            return FirstFrame.Time - CurrentFrame.Time;
        }
    }


    GestureFrame FirstFrame
    {
        get
        {
            return gestureFrames[0];
        }
    }

    GestureFrame CurrentFrame
    {
        get
        {
            return gestureFrames[gestureFrames.Count - 1];
        }
    }

    GestureFrame LastFrame
    {
        get
        {
            return gestureFrames[gestureFrames.Count - 2];
        }
    }

    public override string ToString()
    {
        return string.Format("[Gesture: MousePositionStart={0}, MousePositionCurrent={1}, MousePositionLast={2}, ElapsedTime={3}]", MousePositionStart, MousePositionCurrent, MousePositionLast, ElapsedTime);
    }
}
