using UnityEngine;
using System.Collections;

public interface ILayoutMember : IGameObject {
    void OnLocalLayout (Vector2 newLocalPosition);
}
