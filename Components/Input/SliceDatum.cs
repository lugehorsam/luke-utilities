using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public struct SliceDatum {

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
                RaycastHit? hit = frame.HitForCollider(collider);
                if (hit.HasValue)
                {
                    hitPositions.Add(frame.Position);
                }
                else 
                {
                    Vector3 worldPoint = frame.Position;
                    hitPositions.Add(worldPoint);
                }
            }
            sliceData.Add(new SliceDatum(hitPositions));
        }

        return sliceData.ToArray();
    }

    public Mesh[] SliceMesh(Mesh mesh)
    {
        TriangleDatum[] triangles = TriangleDatum.FromMesh(mesh);
        Debug.Log("Triangles count is " + triangles.Length);
        return null;
    }

    TriangleDatum[] SliceTriangle(TriangleDatum triangle)
    {
        TriangleDatum[] newTriangles = new TriangleDatum[3];
        return null;

//        triangle.EdgeData[0].Vertex1.Y
        //triangle.EdgeData[0].Vertex1;
    }
}
