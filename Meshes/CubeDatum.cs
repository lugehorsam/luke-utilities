using System;
using UnityEngine;

namespace Utilities.Meshes
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
