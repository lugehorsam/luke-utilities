namespace Mesh
{
    using System;

    using UnityEngine;

    [Serializable] public struct CubeDatum
    {
        [SerializeField] private SquareMesh[] squares;

        public CubeDatum(SquareMesh[] squares)
        {
            this.squares = squares;
        }
    }
}
