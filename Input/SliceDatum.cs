using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

/// <summary>
/// https://gamedevelopment.tutsplus.com/tutorials/how-to-dynamically-slice-a-convex-shape--gamedev-14479
/// </summary>
public struct SliceDatum
{
    const float CONNECTION_MARGIN = .0001f;

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
        get { return new ReadOnlyCollection<Vector3>(intersectionVertices); }
    }

    private readonly TriangleDatum[] trianglesToSlice;

    private readonly List<Vector3> intersectionVertices;

    public ISliceable Sliceable { get; private set; }

    private SliceDatum(IList<GestureFrame> gestureFrames, ISliceable sliceable)
    {
        this.gestureFrames = gestureFrames;
        this.intersectionVertices = new List<Vector3>();
        Sliceable = sliceable;

        trianglesToSlice = TriangleDatum.FromMesh(sliceable.Mesh);
        foreach (TriangleDatum triangle in trianglesToSlice)
        {
           // intersectionVertices = GetIntersectionsWithTriangle(triangle);
        }
    }

    public static SliceDatum[] FromGesture(Gesture gesture, ISliceable sliceable)
    {
        List<SliceDatum> sliceData = new List<SliceDatum>();

        Gesture[] collisionGestures = gesture.Filter(
            (frame) =>
            {
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
            var tri2 = CreateSubTriangle(triangleToSlice, triangleToSlice[1], new[] {tri1});
            var tri3 = CreateSubTriangle(triangleToSlice, triangleToSlice[2], new[] {tri1, tri2});
            newTriangles.Add(tri1);
            newTriangles.Add(tri2);
            newTriangles.Add(tri3);
        }
        return newTriangles.ToArray();
    }

    TriangleDatum CreateSubTriangle(TriangleDatum originalTriangle,
        VertexDatum initialVertex,
        TriangleDatum[] otherNewTriangles = null)
    {
        var subTriangle = new TriangleDatum();
        subTriangle[0] = initialVertex;

        var vertexQueue = new Queue<VertexDatum>();
        vertexQueue.Enqueue(intersectionVertices[0]);
        vertexQueue.Enqueue(intersectionVertices[1]);

        var origTriVertices = originalTriangle.Vertices.Except(new[] { initialVertex }).ToArray();
        vertexQueue.Enqueue(origTriVertices[0]);
        vertexQueue.Enqueue(origTriVertices[1]);

        var vertIndex = 1;
        while (vertIndex <= 2)
        {
            var candidateVertex = vertexQueue.Dequeue();
            if (otherNewTriangles != null && otherNewTriangles.Any(
                    (newTri) => newTri.HasEdgeThatIntersects(new EdgeDatum(initialVertex, candidateVertex), CONNECTION_MARGIN)))
            {
                continue;
            }
            subTriangle[vertIndex] = candidateVertex;
            vertIndex++;
        }
        subTriangle.SortVertices();
        return subTriangle;
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
