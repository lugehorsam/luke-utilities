using System;

namespace Utilities.Meshes
{ 

    [Serializable]
    public class SquareMesh : SimpleMesh {

        public SquareMesh(TriangleMesh tri1, TriangleMesh tri2)
        {
            if (!tri1.HasSharedEdge(tri2))
            {
                throw new DataMisalignedException();
            }
            
            _triangles.Add(tri1);
            _triangles.Add(tri2);
        }

        public SquareMesh(float width, float height) : this
        (
                new Vertex(width/2, -height/2, 0f),
                new Vertex(-width/2, -height/2, 0f),
                new Vertex(-width/2, height/2, 0f),
                new Vertex(width/2, height/2, 0)
        ) {}

        public SquareMesh(Vertex upperLeft, Vertex upperRight, Vertex bottomRight, Vertex bottomLeft) : this
        (
            new TriangleMesh
            (
                bottomLeft, 
                upperLeft,
                bottomRight
            ),
            new TriangleMesh
            (
                upperLeft, 
                upperRight,
                bottomRight
            ) 
        ) {}       
    }
}
