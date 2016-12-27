using UnityEngine;
using System;
using System.Linq;

[Serializable]
public struct VertexDatum {
    public float X
    {
        get
        {
            return x;
        }
        set
        {
            x = value;
        }
    }

    public float Y
    {
        get
        {
            return y;
        }
        set
        {
            y = value;
        }
    }

    public float Z
    {
        get
        {
            return z;
        }
        set
        {
            z = value;
        }
    }

    [SerializeField]
    float x;

    [SerializeField]
    float y;

    [SerializeField]
    float z;

    public static implicit operator VertexDatum (Vector3 vector)
    {
        return new VertexDatum(vector.x, vector.y, vector.z);
    }

    public static implicit operator Vector3(VertexDatum vertex)
    {
        return new Vector3(vertex.X, vertex.Y, vertex.Z);
    }

    public static implicit operator VertexDatum (Vector2 vector)
    {
        return new VertexDatum(vector.x, vector.y, 0f);
    }

    public static implicit operator Vector2(VertexDatum vertex)
    {
        return new Vector3(vertex.X, vertex.Y);
    }

    public static VertexDatum operator + (VertexDatum vertex1, VertexDatum vertex2)
    {
        return (Vector3) vertex1 + (Vector3) vertex2;
    }

    public static bool operator > (VertexDatum vertex1, VertexDatum vertex2)
    {
        return vertex1.X + vertex1.Y > vertex2.X + vertex2.Y;
    }

    public static bool operator < (VertexDatum vertex1, VertexDatum vertex2)
    {
        return vertex1.X + vertex1.Y < vertex2.X + vertex2.Y;
    }

    public static VertexDatum operator - (VertexDatum vertex1, VertexDatum vertex2)
    {
        return ((Vector3) vertex1) - ((Vector3) vertex2);
    }

    public static bool operator == (VertexDatum vertex1, VertexDatum vertex2)
    {
        return ((Vector3) vertex1) == ((Vector3) vertex2);
    }

    public static bool operator != (VertexDatum vertex1, VertexDatum vertex2)
    {
        return ((Vector3) vertex1) != ((Vector3) vertex2);
    }


    public VertexDatum(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override string ToString()
    {
        return string.Format("[VertexDatum: X={0}, Y={1}, Z={2}]", X, Y, Z);
    }
}
