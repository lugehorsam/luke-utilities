using System;
using UnityEngine;

namespace Utilities.Meshes
{ 

    [Serializable]
    public class SquareMesh : ProceduralMesh {

        public SquareMesh(TriangleMesh[] triangles)
        {
            if (triangles.Length != 2 || !triangles[0].HasSharedEdge(triangles[1]))
            {
                throw new DataMisalignedException();
            }
            
            TriangleMeshes.AddRange(triangles);
        }

        public SquareMesh(float width, float height)
        {
            Vertex bottomRight = new Vertex(width / 2, -height/2, 0f);
            Vertex bottomLeft = new Vertex(-width / 2, -height/2, 0f);
            Vertex upperLeft = new Vertex(-width / 2, height / 2, 0f);        
            Vertex upperRight = new Vertex(width/2, height/2, 0);
        
            TriangleMeshes.AddRange(new[]
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
