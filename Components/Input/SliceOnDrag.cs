using UnityEngine;

[RequireComponent(typeof(TouchDispatcher))]
public class SliceOnDrag : GameBehavior {

    TouchDispatcher touchDispatcher;
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
        SliceDatum[] sliceData = SliceDatum.FromGesture(currentDrag, collider);
        foreach (SliceDatum slice in sliceData)
        {
            slice.SliceMesh(meshFilter.mesh);
        }

    }
}
