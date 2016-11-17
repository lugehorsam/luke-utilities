using System.Collections.Generic;
using System.Collections.ObjectModel;

public class MotionCollection : AbstractMotion {

    public ReadOnlyCollection<Motion> DragMotions
    {
        get
        {
            return new ReadOnlyCollection<Motion>(motions);
        }
    }

    List<Motion> motions = new List<Motion>();

    public void AddMotion(Motion motion)
    {
        motions.Add(motion);
    }

    public override UnityEngine.Vector3? MousePositionLast
    {
        get
        {
            return LastMotion.MousePositionLast;
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public override UnityEngine.Vector3 MousePositionStart
    {
        get
        {
            return FirstMotion.MousePositionStart;
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public override UnityEngine.Vector3 MousePositionCurrent
    {
        get
        {
            return LastMotion.MousePositionCurrent;

        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public override float? ElapsedTime
    {
        get
        {
            return LastMotion.ElapsedTime - FirstMotion.ElapsedTime;
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    Motion FirstMotion
    {
        get
        {
            return motions[0];
        }
    }

    Motion LastMotion
    {
        get
        {
            return motions[motions.Count - 1];
        }
    }
}
