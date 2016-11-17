using UnityEngine;
using System.Collections;

public class Drag : GestureCollection<DragGesture> {

    public Drag(DragGesture dragGesture) : base(dragGesture)
    {
    }

    public Drag() : base() { }
}
