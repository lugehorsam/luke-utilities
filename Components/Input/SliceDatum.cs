using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public struct SliceDatum {

    /// <summary>
    /// Gets the slice positions in screen coordinates.
    /// </summary>
    /// <value>The slice positions.</value>
    public ReadOnlyCollection<Vector3> SlicePositions
    {
        get
        {
            return new ReadOnlyCollection<Vector3>(slicePositions);
        }
    }

    Vector3[] slicePositions;

    private SliceDatum(IList<Vector3> slicePositions)
    {
        this.slicePositions = slicePositions.ToArray();
    }

    public static SliceDatum[] FromGesture(Gesture gesture, Collider collider)
    {
        List<SliceDatum> sliceData = new List<SliceDatum>();

        Gesture[] collisionGestures = gesture.Filter(
            (frame) => {
               RaycastHit? hit = frame.HitForCollider(collider);
               return hit.HasValue && hit.Value.collider == collider;
            }, 
            includeEdgeFrames: true
        );

        List<Vector3> hitPositions = new List<Vector3>();

        foreach (Gesture collisionGesture in collisionGestures)
        {
            foreach (GestureFrame frame in collisionGesture.GestureFrames)
            {
                hitPositions.Add(frame.Position);
            }
            sliceData.Add(new SliceDatum(hitPositions));
        }

        return sliceData.ToArray();
    }

    public Mesh[] SliceMesh(Mesh mesh)
    {
        TriangleDatum[] triangles = TriangleDatum.FromMesh(mesh);
        SliceTriangle(triangles[0]);
        return null;
    }

    TriangleDatum[] SliceTriangle(TriangleDatum triangle)
    {
        Vector3? intersectionPoint = triangle.EdgeData[0].GetIntersection(new EdgeDatum(slicePositions));
        Diagnostics.Log("Intersesction point is " + intersectionPoint);
        TriangleDatum[] newTriangles = new TriangleDatum[3];
        return null;

//        triangle.EdgeData[0].Vertex1.Y
        //triangle.EdgeData[0].Vertex1;
    }
}
