using UnityEngine;
using System;

/**
namespace Utilities.Input
{
    [RequireComponent(typeof(TouchDispatcher))]
    public class SliceOnDrag : MonoBehaviour, ISliceable {

        public Action<SliceDatum> OnSlice = (slice) => { };
        TouchDispatcher touchDispatcher;

        public BoxCollider2D BoxCollider2D
        {
            get { return collider; }
        }

        public Mesh Mesh
        {
            get { return meshFilter.mesh; }
        }
        new BoxCollider2D collider;
        MeshFilter meshFilter;

        [SerializeField]
        bool debugMode;

         void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            touchDispatcher = GetComponent<TouchDispatcher>();
            collider = GetComponent<BoxCollider2D>();
            touchDispatcher.OnDragLeave += OnDragLeave;
        }

        void OnDestroy()
        {
            touchDispatcher.OnDragLeave -= OnDragLeave;
        }

        void OnDragLeave(TouchDispatcher dispatcher, Gesture currentDrag)
        {
            SliceDatum[] sliceData = SliceDatum.FromGesture(currentDrag, this);

            foreach (SliceDatum slice in sliceData)
            {
                OnSlice(slice);
            }
        }
    }
}
**/