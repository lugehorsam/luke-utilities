using System.Collections.Generic;
using System.Collections.ObjectModel;

public class GestureCollection<T> : AbstractGesture where T : Gesture { 

    public ReadOnlyCollection<T> DragGestures
    {
        get
        {
            return new ReadOnlyCollection<T>(gestures);
        }
    }

    List<T> gestures = new List<T>();

    public void AddGesture(T gesture)
    {
        gestures.Add(gesture);
    }

    public override UnityEngine.Vector3 MousePositionLast
    {
        get
        {
            return LastGesture.MousePositionLast;
        }
    }

    public override UnityEngine.Vector3 MousePositionStart
    {
        get
        {
            return FirstGesture.MousePositionStart;
        }
    }

    public override UnityEngine.Vector3 MousePositionCurrent
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

    public T FirstGesture
    {
        get
        {
            return gestures[0];
        }
    }

    public T LastGesture
    {
        get
        {
            return gestures[gestures.Count - 1];
        }
    }

    public GestureCollection(T firstGesture)
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
