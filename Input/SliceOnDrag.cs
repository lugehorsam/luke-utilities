using UnityEngine;
using System;

namespace Slicing
{

    [RequireComponent(typeof(TouchDispatcher))]
    public class SliceOnDrag : MonoBehaviour, ISliceable {

        public Action<SliceDatum> OnSlice = (slice) => { };
        TouchDispatcher touchDispatcher;

        public Collider Collider
        {
            get { return collider; }
        }

        public Mesh Mesh
        {
            get { return meshFilter.mesh; }
        }
        new Collider collider;
        MeshFilter meshFilter;

        [SerializeField]
        bool debugMode;

         void Awake()
        {
            meshFilter = GetComponent<MeshFilter>();
            touchDispatcher = GetComponent<TouchDispatcher>();
            collider = GetComponent<Collider>();
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