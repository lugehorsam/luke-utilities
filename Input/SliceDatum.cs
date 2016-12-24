using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public struct SliceDatum {

    readonly IList<GestureFrame> gestureFrames;

    public Vector3[] SlicePositions
    {
        get
        {
            List<Vector3> hitPositions = new List<Vector3>();

            for (int i = 0; i < gestureFrames.Count; i++)
            {
                hitPositions.Add(gestureFrames[i].Position);
            }

            return hitPositions.ToArray();
        }
    }

    public ReadOnlyCollection<Vector3> IntersectionPoints
    {
        get
        {
            return new ReadOnlyCollection<Vector3>(intersectionPoints);
        }
    }

    private readonly List<Vector3> intersectionPoints;

    public ISliceable Sliceable { get; private set; }

    private SliceDatum(IList<GestureFrame> gestureFrames, ISliceable sliceable)
    {
        this.gestureFrames = gestureFrames;
        this.intersectionPoints = new List<Vector3>();
        Sliceable = sliceable;

        TriangleDatum[] triangles = TriangleDatum.FromMesh(sliceable.Mesh);
        List<Vector3> intersectionPoints = new List<Vector3>();
        foreach (TriangleDatum triangle in triangles)
        {
            Vector3? intersectionWithTriangle = GetIntersectionWithTriangle(triangle);
            if (intersectionWithTriangle.HasValue)
            {
                intersectionPoints.Add(intersectionWithTriangle.Value);
            }
        }
    }

    public static SliceDatum[] FromGesture(Gesture gesture, ISliceable sliceable)
    {
        List<SliceDatum> sliceData = new List<SliceDatum>();

        Gesture[] collisionGestures = gesture.Filter(
            (frame) => {
               RaycastHit? hit = frame.HitForCollider(sliceable.Collider);
               return hit.HasValue && hit.Value.collider == sliceable.Collider;
            }, 
            includeEdgeFrames: true
        );

        foreach (Gesture collisionGesture in collisionGestures)
        {
            SliceDatum datum = new SliceDatum(collisionGesture.GestureFrames, sliceable);
            sliceData.Add(datum);
        }
        return sliceData.ToArray();
    }

    TriangleDatum[] SliceTriangle(TriangleDatum triangle)
    {
        TriangleDatum[] newTriangles = new TriangleDatum[3];
        return null;
    }

    public Vector3? GetIntersectionWithTriangle(TriangleDatum triangle)
    {
        foreach (EdgeDatum edge in triangle.EdgeData)
        {
            Vector3? intersectionPoint = edge.GetIntersectionWithEdge(new EdgeDatum(SlicePositions));
            if (intersectionPoint.HasValue)
            {
                return intersectionPoint;
            }
        }
        return null;
    }
}
