using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class GestureCollection : AbstractGesture { 

    public ReadOnlyCollection<AbstractGesture> DragGestures
    {
        get
        {
            return new ReadOnlyCollection<AbstractGesture>(gestures);
        }
    }

    List<AbstractGesture> gestures = new List<AbstractGesture>();

    public void AddGesture(AbstractGesture gesture)
    {
        gestures.Add(gesture);
    }

    public override Vector3? MousePositionLast
    {
        get
        {
            return LastGesture.MousePositionLast;
        }
    }

    public override Vector3 MousePositionStart
    {
        get
        {
            return FirstGesture.MousePositionStart;
        }
    }

    public override Vector3 MousePositionCurrent
    {
        get
        {
            return LastGesture.MousePositionCurrent;

        }
    }

    public override float ElapsedTime
    {
        get
        {
            return LastGesture.ElapsedTime;
        }
    }

    public AbstractGesture FirstGesture
    {
        get
        {
            return gestures[0];
        }
    }

    public AbstractGesture LastGesture
    {
        get
        {
            return gestures[gestures.Count - 1];
        }
    }

    public GestureCollection(AbstractGesture firstGesture)
    {
        AddGesture(firstGesture);
    }

    public GestureCollection()
    {
    }

    public override string ToString()
    {
        return string.Format("[GestureCollection: DragGestures={0}, MousePositionLast={1}, MousePositionStart={2}, MousePositionCurrent={3}, ElapsedTime={4}, FirstGesture={5}, LastGesture={6}]", DragGestures, MousePositionLast, MousePositionStart, MousePositionCurrent, ElapsedTime, FirstGesture, LastGesture);
    }
}
