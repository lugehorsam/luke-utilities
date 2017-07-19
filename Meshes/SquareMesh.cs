using System;

namespace Utilities.Meshes
{ 

    [Serializable]
    public class SquareMesh : SimpleMesh {

        public SquareMesh(TriangleMesh[] triangles)
        {
            if (triangles.Length != 2 || !triangles[0].HasSharedEdge(triangles[1]))
            {
                throw new DataMisalignedException();
            }
            
            _triangles.AddRange(triangles);
        }

        public SquareMesh(float width, float height)
        {
            Vertex bottomRight = new Vertex(width / 2, -height/2, 0f);
            Vertex bottomLeft = new Vertex(-width / 2, -height/2, 0f);
            Vertex upperLeft = new Vertex(-width / 2, height / 2, 0f);        
            Vertex upperRight = new Vertex(width/2, height/2, 0);
        
            _triangles.AddRange(new[]
            {
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
            });
        }
    }
}
