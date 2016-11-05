using UnityEngine;
using System.Collections;

public class InfiniteLerp<TProperty>
    where TProperty : struct
{
    public TProperty UnitsPerSecond
    {
        get;
        private set;
    }
    public InfiniteLerp(TProperty unitsPerSecond)
    {
        this.UnitsPerSecond = unitsPerSecond;
    }
}
