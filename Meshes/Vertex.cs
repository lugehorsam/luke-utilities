using System;
using UnityEngine;

namespace Utilities.Meshes
{
     
    [Serializable]
    public class Vertex {

        public Vector3 AsVector3
        {
            get
            {
                return new Vector3(x, y, z);
            }
        }
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
    
        public Vertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public Vertex(Vector3 vector3)
        {
            this.x = vector3.x;
            this.y = vector3.y;
            this.z = vector3.z;
        }
    
        public override string ToString()
        {
            return string.Format("[VertexDatum: X={0}, Y={1}, Z={2}]", X, Y, Z);
        }        
    }   
}