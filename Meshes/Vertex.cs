using System;
using System.Linq;
using UnityEngine;

namespace Utilities.Meshes
{
     
    [Serializable]
    public class Vertex {

        public Vector3 AsVector3 => new Vector3(x, y, z);

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
    
        float x, y, z;
    
        public Vertex(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public Vertex(Vector3 vector3)
        {
            Set(vector3);
        }

        public void Set(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }
    
        public override string ToString()
        {
            return $"[VertexDatum: X={X}, Y={Y}, Z={Z}]";
        }       
    }   
}