using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class GameEdge : CollectedBehavior<GameEdge, List<GameEdge>> {

    public GameNode node1 {
        get;
        set;
    }
    public GameNode node2 {
        get;
        set;
    }

    public bool Contains(GameNode node) {
        return node == node1 || node == node2;
    }

    public abstract void DrawConnection(GameNode startNode, GameNode endNode);
}
