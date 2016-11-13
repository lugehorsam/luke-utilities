using System;
using UnityEngine;

[Serializable]
public struct CubeDatum {

    [SerializeField]
    SquareDatum[] squares;

    public CubeDatum(SquareDatum[] squares)
    {
        this.squares = squares;
    }
}
