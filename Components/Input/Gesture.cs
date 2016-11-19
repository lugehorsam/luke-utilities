using UnityEngine;
using System.Collections;

public class Gesture : AbstractGesture {

    public sealed override Vector3 MousePositionStart
    {
        get
        {
            return mousePositionStart;
        }
    }

    Vector3 mousePositionStart;

    public sealed override Vector3 MousePositionCurrent
    {
        get
        {
            return mousePositionCurrent;
        }
    }

    Vector3 mousePositionCurrent;

    public sealed override Vector3? MousePositionLast
    {
        get
        {
            return mousePositionLast;
        }
    }

    Vector3? mousePositionLast;


    public override float ElapsedTime
    {
        get
        {
            return elapsedTime;
        }
    }

    float elapsedTime;

    public Gesture(Vector3 mousePositionStart)
    {
        this.mousePositionStart = mousePositionStart;
    }

    public void SetMousePositionStart(Vector3 mousePositionStart)
    {
        this.mousePositionStart = mousePositionStart;
    }

    public void SetMousePositionCurrent(Vector3 mousePositionCurrent)
    {
        Vector3 oldCurrent = this.mousePositionCurrent;
        this.mousePositionCurrent = mousePositionCurrent;
        this.mousePositionLast = oldCurrent;
    }

    public void IncrementElapsedTime(float elapsedTime)
    {
        this.elapsedTime += elapsedTime;
    }

    public override string ToString()
    {
        return string.Format("[Gesture: MousePositionStart={0}, MousePositionCurrent={1}, MousePositionLast={2}, ElapsedTime={3}]", MousePositionStart, MousePositionCurrent, MousePositionLast, ElapsedTime);
    }
}
