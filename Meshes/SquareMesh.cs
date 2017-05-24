using System;
using UnityEngine;

namespace Utilities.Meshes
{ 

    [Serializable]
    public class SquareMesh {

        private readonly TriangleDatum[] triangles;

        public SquareMesh(TriangleDatum[] triangles)
        {
            if (triangles.Length != 2 || !triangles[0].HasSharedEdge(triangles[1]))
            {
                throw new DataMisalignedException();
            }
            this.triangles = triangles;
        }

        public SquareMesh(float width, float height)
        {
            Vector3 bottomRight = new Vector3(width / 2, -height/2, 0f);
            Vector3 bottomLeft = new Vector3(-width / 2, -height/2, 0f);
            Vector3 upperLeft = new Vector3(-width / 2, height / 2, 0f);        
            Vector3 upperRight = new Vector3(width/2, height/2, 0);
        
            triangles = new[]
            {
                new TriangleDatum
                (
                    bottomLeft, 
                    upperLeft,
                    bottomRight
                ),
                new TriangleDatum
                (
                    upperLeft, 
                    upperRight,
                    bottomRight
                )
            };
        }

        public Mesh ToMesh()
        {
            return TriangleDatum.ToMesh(triangles);
        }
    }
}
