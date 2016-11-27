using UnityEngine;
using System.Collections;

public struct EdgeDatum {
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

    public EdgeDatum(VertexDatum vertex1, VertexDatum vertex2)
    {
        this.Vertex1 = vertex1;
        this.Vertex2 = vertex2;
    }

    public Vector3? GetIntersection(EdgeDatum otherEdge)
    {
        float thisYDiff = Vertex2.Y - Vertex1.Y;
        float thisXDiff = Vertex1.X - Vertex2.X;
        float thisC = thisYDiff * this.Vertex1.X + thisXDiff * this.Vertex1.Y;

        float otherYDiff = otherEdge.Vertex2.Y - otherEdge.Vertex1.Y;
        float otherXDiff = otherEdge.Vertex1.X - otherEdge.Vertex2.X;
        float otherC = otherYDiff * otherEdge.Vertex1.X + otherXDiff * otherEdge.Vertex1.Y;

        float det = thisYDiff * otherXDiff - otherYDiff * thisXDiff;

        if (det == 0f)
        {
            return null;
        }
        else {
            float x = (otherXDiff * thisC - thisXDiff * otherC) / det;
            float y = (thisYDiff * otherC - otherYDiff * thisC) / det;
            return new Vector3(x, y, 0f);
        }
    }
}
