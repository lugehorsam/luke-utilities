using System;
using UnityEngine;

namespace Mesh
{

    [Serializable]
    public struct CubeDatum {

        [SerializeField]
        SquareMesh[] squares;

        public CubeDatum(SquareMesh[] squares)
        {
            this.squares = squares;
        }
    }
}
