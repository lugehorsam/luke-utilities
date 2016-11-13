﻿using UnityEngine;
using System.Collections;

public class ColliderResizer : GameBehavior {
    private BoxCollider2D boxCollider;
    private MeshRenderer meshRenderer;

    [SerializeField]
    private float xPadding;
    [SerializeField]
    private float yPadding;

    protected override void InitComponents()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        meshRenderer = GetComponent<MeshRenderer>();
    }     

    public void Resize () {
        Vector2 paddedSize = meshRenderer.GetScaledSize();
        paddedSize.x += xPadding;
        paddedSize.y += yPadding;
        boxCollider.size = paddedSize;
        boxCollider.offset = new Vector2(boxCollider.size.x/2, -boxCollider.size.y/2);
    }
}
