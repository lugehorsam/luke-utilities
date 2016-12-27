using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// https://gamedevelopment.tutsplus.com/tutorials/how-to-dynamically-slice-a-convex-shape--gamedev-14479
/// </summary>
public struct SliceDatum {

    readonly IList<GestureFrame> gestureFrames;

    /// <summary>
    /// In local coordinates relative to the triangle it sliced.
    /// </summary>
    public Vector3[] SlicePositions
    {
        get
        {
            List<Vector3> hitPositions = new List<Vector3>();

            for (int i = 0; i < gestureFrames.Count; i++)
            {
                Vector3 worldPoint = Camera.main.ScreenToWorldPoint(gestureFrames[i].Position);
                Vector3 localPoint = Sliceable.Collider.transform.InverseTransformVector(worldPoint);
                hitPositions.Add(localPoint);
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

    private readonly TriangleDatum[] trianglesToSlice;

    private readonly List<Vector3> intersectionPoints;

    public ISliceable Sliceable { get; private set; }

    private SliceDatum(IList<GestureFrame> gestureFrames, ISliceable sliceable)
    {
        this.gestureFrames = gestureFrames;
        this.intersectionPoints = new List<Vector3>();
        Sliceable = sliceable;

        trianglesToSlice = TriangleDatum.FromMesh(sliceable.Mesh);
        foreach (TriangleDatum triangle in trianglesToSlice)
        {
            intersectionPoints = GetIntersectionsWithTriangle(triangle);
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

    public TriangleDatum[] ApplySlice()
    {
        var newTriangles = new List<TriangleDatum>();
        foreach (var triangleToSlice in trianglesToSlice)
        {
            var tri1 = CreateSubTriangle(triangleToSlice, triangleToSlice[0]);
            var tri2 = CreateSubTriangle(triangleToSlice, triangleToSlice[1]);
            var tri3 = CreateSubTriangle(triangleToSlice, triangleToSlice[2]);
            newTriangles.Add(tri1);
            newTriangles.Add(tri2);
            newTriangles.Add(tri3);
        }
        return newTriangles.ToArray();
    }

    TriangleDatum CreateSubTriangle(TriangleDatum originalTriangle, VertexDatum initialVertex)
    {
        var triangle = new TriangleDatum();
        triangle[0] = initialVertex;

        var otherCandidates = originalTriangle.Vertices.Except(new[] {initialVertex}).ToArray();

        var vertIndex = 1;
        foreach (var candidate in otherCandidates)
        {
            var candidateEdge = new EdgeDatum(initialVertex, candidate);

            VertexDatum? intersectionOnEdge = null;
            foreach (var intersectionPoint in intersectionPoints)
            {
                if (candidateEdge.VertexLiesOnEdge(intersectionPoint))
                {
                    intersectionOnEdge = intersectionPoint;
                    break;
                }
            }

            if (intersectionOnEdge.HasValue)
            {
                triangle[vertIndex] = intersectionOnEdge.Value;
            }
            else
            {
                triangle[vertIndex] = candidate;
            }
            vertIndex++;
        }
        //triangle.SortVertices();
        return triangle;
    }

    public List<Vector3> GetIntersectionsWithTriangle(TriangleDatum triangle)
    {
        var intersectionPoints = new List<Vector3>();
        foreach (EdgeDatum edge in triangle.EdgeData)
        {
            Vector3? intersectionPoint = edge.GetIntersectionWithEdge(new EdgeDatum(SlicePositions));
            if (intersectionPoint.HasValue)
            {
                intersectionPoints.Add(intersectionPoint.Value);
            }
        }
        return intersectionPoints;
    }
}
