using UnityEngine;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(TouchDispatcher))]
public class SliceOnDrag : GameBehavior, ISliceable {

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

    protected override void InitComponents()
    {
        meshFilter = GetComponent<MeshFilter>();
        touchDispatcher = GetComponent<TouchDispatcher>();
        collider = GetComponent<Collider>();
    }

    protected sealed override void AddEventHandlers()
    {
        touchDispatcher.OnDragLeave += OnDragLeave;
    }

    protected sealed override void RemoveEventHandlers()
    {
        touchDispatcher.OnDragLeave -= OnDragLeave;
    }

    void OnDragLeave(TouchDispatcher dispatcher, Gesture currentDrag)
    {
        SliceDatum[] sliceData = SliceDatum.FromGesture(currentDrag, this);
        Diagnostics.Log("Slice data length is " + sliceData);
        foreach (SliceDatum slice in sliceData)
        {
            OnSlice(slice);
        }
    }
}
