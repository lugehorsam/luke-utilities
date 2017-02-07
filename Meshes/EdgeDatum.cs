using System;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public struct EdgeDatum
{
    private const float POINT_ON_EDGE_TOLERANCE = .01f;

    public ReadOnlyCollection<VertexDatum> Vertices
    {
        get
        {
            return new ReadOnlyCollection<VertexDatum>(new []{Vertex1, Vertex2});
        }
    }

    public VertexDatum Vertex1
    {
        get;
        private set;
    }

    public VertexDatum Vertex2
    {
        get;
        private set;
    }

    public float MinY
    {
        get
        {
            return Mathf.Min(Vertex1.Y, Vertex2.Y);
        }
    }

    public float MaxY
    {
        get
        {
            return Mathf.Max(Vertex1.Y, Vertex2.Y);
        }
    }

    public float MinX
    {
        get
        {
            return Mathf.Min(Vertex1.X, Vertex2.X);
        }
    }

    public float MaxX
    {
        get
        {
            return Mathf.Max(Vertex1.X, Vertex2.X);
        }
    }

    public bool VertexLiesOnEdge(VertexDatum vertex)
    {
        Vector3 combinedMidpointDist = (Vertex1 - vertex) + (vertex - Vertex2);
        Vector3 endpointDist = (Vertex1 - Vertex2);
        bool vertexWithinEdgeRect = VertexWithinEdgeRect(vertex);
        bool liesOnEdge = combinedMidpointDist == endpointDist && vertexWithinEdgeRect;
        return liesOnEdge;
    }

    public bool VertexWithinEdgeRect(VertexDatum vertex)
    {
        return MinX.ApproximatelyLessThan(vertex.X, POINT_ON_EDGE_TOLERANCE) &&
               vertex.X.ApproximatelyLessThan(MaxX, POINT_ON_EDGE_TOLERANCE) &&
               MinY.ApproximatelyLessThan(vertex.Y, POINT_ON_EDGE_TOLERANCE) &&
               vertex.Y.ApproximatelyLessThan(MaxY, POINT_ON_EDGE_TOLERANCE);
    }

    public EdgeDatum(VertexDatum vertex1, VertexDatum vertex2)
    {
        Vertex1 = vertex1;
        Vertex2 = vertex2;
    }

    public EdgeDatum(IList<Vector3> vectorList)
    {
        Vertex1 = vectorList.First();
        Vertex2 = vectorList.Last();
    }

    /// <summary>
    /// </summary>
    /// <param name="otherEdge"></param>
    /// <param name="connectionMargin"></param>
    /// <returns></returns>
    public bool HasIntersectionWithEdge(EdgeDatum otherEdge, bool onThisEdge = true, bool onOtherEdge = true)
    {
        VertexDatum? intersectionPoint = GetIntersectionWithEdge(otherEdge, onThisEdge, onOtherEdge);
        return intersectionPoint.HasValue;
    }

    public bool ConnectsToEdge(EdgeDatum otherEdge, float acceptableDifference = 0f)
    {
        EdgeDatum thisEdge = this;
        bool isConnection = otherEdge.Vertices.Any(
            (otherVertex) => otherVertex == thisEdge.Vertex1 || otherVertex == thisEdge.Vertex2);
        return isConnection;
    }

    /// <summary>
    /// See https://www.topcoder.com/community/array-science/array-science-tutorials/geometry-concepts-line-intersection-and-its-applications
    /// Returns false if edges are connected at the point of intersection.
    /// </summary>
    /// <param name="otherEdge"></param>
    /// <param name="connectionMargin">Any vertices whose distance from one another
    /// fall under the connection margin will not belong to edges that can intersect one another.
    /// </param>
    /// <returns></returns>
    public Vector3? GetIntersectionWithEdge(EdgeDatum otherEdge, bool onThisEdge = true, bool onOtherEdge = true)
    {
        if (otherEdge.ConnectsToEdge(this))
        {
            return null;
        }

        float thisYDiff = Vertex2.Y - Vertex1.Y;
        float thisXDiff = Vertex1.X - Vertex2.X;
        double thisC = thisYDiff * Vertex1.X + thisXDiff * Vertex1.Y;

        float otherYDiff = otherEdge.Vertex2.Y - otherEdge.Vertex1.Y;
        float otherXDiff = otherEdge.Vertex1.X - otherEdge.Vertex2.X;
        double otherC = otherYDiff * otherEdge.Vertex1.X + otherXDiff * otherEdge.Vertex1.Y;

        double det = thisYDiff * otherXDiff - otherYDiff * thisXDiff;

        if (det != 0f)
        {
            double x = (otherXDiff * thisC - thisXDiff * otherC) / det;
            double y = (thisYDiff * otherC - otherYDiff * thisC) / det;
            Vector3 intersectionPoint = new Vector3((float) x, (float) y, 0f);
            if ((!onThisEdge || VertexLiesOnEdge(intersectionPoint)) &&
                (!onOtherEdge || otherEdge.VertexLiesOnEdge(intersectionPoint)))
            {
                return intersectionPoint;
            }
        }

        return null;
    }

    public override string ToString()
    {
        return string.Format("[EdgeDatum: Vertex1={0}, Vertex2={1}, MinY={2}, MaxY={3}, MinX={4}, MaxX={5}]", Vertex1, Vertex2, MinY, MaxY, MinX, MaxX);
    }
}
