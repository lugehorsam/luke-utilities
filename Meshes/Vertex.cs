using System;

using UnityEngine;

namespace Mesh
{
    [Serializable] public class Vertex
    {
        public Vector3 AsVector3 => new Vector3(_x, _y, _z);

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public float Z
        {
            get { return _z; }
            set { _z = value; }
        }

        [SerializeField] private float _x, _y, _z;

        public Vertex(float x, float y, float z)
        {
            this._x = x;
            this._y = y;
            this._z = z;
        }

        public Vertex(Vector3 vector3)
        {
            Set(vector3);
        }

        public void Set(Vector3 vector3)
        {
            _x = vector3.x;
            _y = vector3.y;
            _z = vector3.z;
        }

        public override string ToString()
        {
            return $"[VertexDatum: X={X}, Y={Y}, Z={Z}]";
        }
    }
}