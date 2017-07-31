using UnityEngine;

namespace Utilities
{
    public interface ILayoutMember : IGameObject {

        RectTransform RectTransform { get; }
        void OnLocalLayout (Vector2 newLocalPosition);
    }
}
