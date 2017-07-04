using UnityEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Linq;

public class Gesture {

    public Vector2 MousePositionStart
    {
        get
        {
            return FirstFrame.Position;
        }
    }

    public Vector2 MousePositionCurrent
    {
        get
        {
            return CurrentFrame.Position;
        }
    }

    public Vector2? MousePositionLast
    {
        get
        {
            if (LastFrame.HasValue)
            {
                return LastFrame.Value.Position;
            }
            return null;
        }
    }

    public float ElapsedTime
    {
        get
        {
            return FirstFrame.Time - CurrentFrame.Time;
        }
    }

    public ReadOnlyCollection<GestureFrame> GestureFrames
    {
        get
        {
            return new ReadOnlyCollection<GestureFrame>(gestureFrames);
        }
    }

    public Vector2 Velocity
    {
        get
        {
            return ElapsedTime > 0f ? (MousePositionCurrent - MousePositionStart) / ElapsedTime : Vector2.zero;
        }
    }

    public void AddGestureFrame(GestureFrame frame)
    {
        gestureFrames.Add(frame);
    }

    public GestureFrame FirstFrame
    {
        get
        {
            return gestureFrames[0];
        }
    }

    public GestureFrame CurrentFrame
    {
        get
        {
            return gestureFrames[gestureFrames.Count - 1];
        }
    }

    public GestureFrame? LastFrame
    {
        get
        {
            if (gestureFrames.Count <= 1)
            {
                return null;
            }
            
            return gestureFrames[gestureFrames.Count - 2];
        }
    }

    protected List<GestureFrame> gestureFrames = new List<GestureFrame>();

    public Gesture(IList<GestureFrame> frames)
    {
        gestureFrames.AddRange(frames);
    }

    public Gesture()
    {

    }

    public Gesture[] Filter(Func<GestureFrame, bool> filterCriteria, bool includeEdgeFrames = false)
    {
        List<Gesture> subGestures = new List<Gesture>();
        Gesture currentGesture = null;

        for (int frameIndex = 0; frameIndex < gestureFrames.Count; frameIndex++)
        {
            if (filterCriteria(gestureFrames[frameIndex]))
            {
                if (currentGesture == null)
                {
                    currentGesture = new Gesture();
                    if (includeEdgeFrames && frameIndex > 0)
                    {
                        currentGesture.AddGestureFrame((gestureFrames[frameIndex - 1]));
                    }
                }

                currentGesture.AddGestureFrame(gestureFrames[frameIndex]);
            }
            else if (currentGesture != null) {
                if (includeEdgeFrames && frameIndex < gestureFrames.Count - 1)
                {
                    currentGesture.AddGestureFrame((gestureFrames[frameIndex + 1]));
                }
                subGestures.Add(currentGesture);
                currentGesture = null;
            }
        }

        return subGestures.ToArray();
    }
}
