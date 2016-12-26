using System.Collections.Generic;
using System;
using UnityEngine;

public class VertexCycleComparer : IComparer<VertexDatum>
{
    private readonly CycleDirection cycleDirection;

    public int Compare(VertexDatum datum1, VertexDatum datum2)
    {
        float angle1 = Vector2.Angle(Vector2.up, datum1);
        float angle2 = Vector2.Angle(Vector2.up, datum2);
        int valueToReturn;
        if (cycleDirection == CycleDirection.Clockwise)
        {
            valueToReturn = angle1 < angle2 ? -1 : 1;
        }
        else
        {
            valueToReturn = angle1 < angle2 ? 1 : -1;
        }

        Diagnostics.Log("1 is " + datum1 + " , " + angle1 + " 2 is " + datum2 + " , " + angle2 + " value to return " + valueToReturn);
        return valueToReturn;
    }

    public VertexCycleComparer(CycleDirection cycleDirection)
    {
        this.cycleDirection = cycleDirection;
    }
}
