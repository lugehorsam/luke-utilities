using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public struct EdgeDatum {

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
        return MinX <= vertex.X && vertex.X <= MaxX
            && MinY <= vertex.Y && vertex.Y <= MaxY;
    }

    public EdgeDatum(VertexDatum vertex1, VertexDatum vertex2)
    {
        this.Vertex1 = vertex1;
        this.Vertex2 = vertex2;
    }

    public EdgeDatum(IList<Vector3> vectorList)
    {
        this.Vertex1 = vectorList.First();
        this.Vertex2 = vectorList.Last();
    }

    /// <summary>
    /// See https://www.topcoder.com/community/data-science/data-science-tutorials/geometry-concepts-line-intersection-and-its-applications
    /// </summary>
    /// <param name="otherEdge"></param>
    /// <returns></returns>
    public Vector3? GetIntersectionWithEdge(EdgeDatum otherEdge)
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
            Vector3 intersectionPoint = new Vector3(x, y, 0f);
            if (VertexLiesOnEdge(intersectionPoint))
            {
                Diagnostics.Log("returning intersection point" + intersectionPoint);
                return intersectionPoint;
            }
            else
            {
                return null;
            }
        }
    }

    public override string ToString()
    {
        return string.Format("[EdgeDatum: Vertex1={0}, Vertex2={1}, MinY={2}, MaxY={3}, MinX={4}, MaxX={5}]", Vertex1, Vertex2, MinY, MaxY, MinX, MaxX);
    }
}
