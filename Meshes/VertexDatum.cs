using UnityEngine;
using System;

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
