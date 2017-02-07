using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
public abstract class GameNode : CollectedClass<GameNode> {

    [SerializeField]
    GameEdge edgePrefab;

    public void ConnectTo(GameNode[] nodes) {
        for (int i = 0; i < nodes.Length; i++) {
            ConnectTo (nodes [i]);
        }
    }

    public virtual void ConnectTo (GameNode node) {
        GameEdge edge = GameObject.Instantiate (edgePrefab);
        edge.transform.SetParent (transform, worldPositionStays: false);
        edge.transform.localPosition = Vector3.zero;
        edge.node1 = this;
        edge.node2 = node;
        edge.DrawConnection(edge.node1, edge.node2);
        OnConnectTo (edge);
    }
        
    protected abstract void OnConnectTo (GameEdge edge);
}

**/