﻿namespace Mesh
{
    using System;

    [Serializable] public class SquareMesh : SimpleProceduralMesh
    {
        public SquareMesh(TriangleMesh tri1, TriangleMesh tri2)
        {
            if (!tri1.HasSharedEdge(tri2))
            {
                throw new DataMisalignedException();
            }

            _triangles.Add(tri1);
            _triangles.Add(tri2);
        }

        public SquareMesh(float width, float height) : this(new Vertex(width / 2, -height / 2, 0f), new Vertex(-width / 2, -height / 2, 0f), new Vertex(-width / 2, height / 2, 0f), new Vertex(width / 2, height / 2, 0)) { }

        public SquareMesh(Vertex upperLeft, Vertex upperRight, Vertex bottomRight, Vertex bottomLeft) : this(new TriangleMesh(bottomLeft, upperLeft, bottomRight), new TriangleMesh(upperLeft, upperRight, bottomRight)) { }

        public float Width
        {
            get { return _UpperRightVertex.X - _UpperLeftVertex.X; }
            set
            {
                _UpperRightVertex.X = value;
                _LowerRightVertex.X = value;
            }
        }

        public float Height
        {
            get { return _UpperRightVertex.Y - _UpperLeftVertex.Y; }
            set
            {
                _LowerLeftVertex.Y = value;
                _LowerRightVertex.Y = value;
            }
        }

        private Vertex _UpperLeftVertex
        {
            get { return GetSortedVertices()[0]; }
        }

        private Vertex _UpperRightVertex
        {
            get { return GetSortedVertices()[1]; }
        }

        private Vertex _LowerRightVertex
        {
            get { return GetSortedVertices()[2]; }
        }

        private Vertex _LowerLeftVertex
        {
            get { return GetSortedVertices()[3]; }
        }
    }
}
