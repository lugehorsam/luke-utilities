namespace Utilities.Gesture
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using UnityEngine;

    public class Gesture
    {
        public Vector2 MousePositionStart
        {
            get { return FirstFrame.Position; }
        }

        public Vector2 MousePositionCurrent
        {
            get { return CurrentFrame.Position; }
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
            get { return CurrentFrame.Time - FirstFrame.Time; }
        }

        public ReadOnlyCollection<GestureFrame> GestureFrames
        {
            get { return new ReadOnlyCollection<GestureFrame>(_gestureFrames); }
        }

        public Vector2 Velocity
        {
            get { return ElapsedTime > 0f ? (MousePositionCurrent - MousePositionStart) / ElapsedTime : Vector2.zero; }
        }

        public void Add(GestureFrame frame)
        {
            _gestureFrames.Add(frame);
        }

        public GestureFrame FirstFrame
        {
            get { return _gestureFrames[0]; }
        }

        public GestureFrame CurrentFrame
        {
            get { return _gestureFrames[_gestureFrames.Count - 1]; }
        }

        public GestureFrame? LastFrame
        {
            get
            {
                if (_gestureFrames.Count <= 1)
                {
                    return null;
                }

                return _gestureFrames[_gestureFrames.Count - 2];
            }
        }

        protected List<GestureFrame> _gestureFrames = new List<GestureFrame>();

        public Gesture(IList<GestureFrame> frames)
        {
            _gestureFrames.AddRange(frames);
        }

        public Gesture() { }

        public Gesture[] Filter(Func<GestureFrame, bool> filterCriteria, bool includeEdgeFrames = false)
        {
            var subGestures = new List<Gesture>();
            Gesture currentGesture = null;

            for (var frameIndex = 0; frameIndex < _gestureFrames.Count; frameIndex++)
            {
                if (filterCriteria(_gestureFrames[frameIndex]))
                {
                    if (currentGesture == null)
                    {
                        currentGesture = new Gesture();
                        if (includeEdgeFrames && (frameIndex > 0))
                        {
                            currentGesture.Add(_gestureFrames[frameIndex - 1]);
                        }
                    }

                    currentGesture.Add(_gestureFrames[frameIndex]);
                }
                else if (currentGesture != null)
                {
                    if (includeEdgeFrames && (frameIndex < _gestureFrames.Count - 1))
                    {
                        currentGesture.Add(_gestureFrames[frameIndex + 1]);
                    }
                    subGestures.Add(currentGesture);
                    currentGesture = null;
                }
            }

            return subGestures.ToArray();
        }
    }
}
