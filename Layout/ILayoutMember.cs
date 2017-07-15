using UnityEngine;
using System.Collections;

public interface ILayoutMember : IGameObject {

    RectTransform RectTransform { get; }
    void OnLocalLayout (Vector2 newLocalPosition);
}
