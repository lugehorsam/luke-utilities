using UnityEngine;
using System.Linq;

public struct GestureFrame {

    public Vector2 Position
    {
        get
        {
            return position;
        }
    }
    Vector2 position;

    RaycastHit? hitInfo;

    public float Time
    {
        get
        {
            return time;
        }
    }
    float time;

    public GestureFrame(Vector2 position, RaycastHit? hitInfo = null)
    {
        this.position = position;
        this.hitInfo = hitInfo;
        this.time = UnityEngine.Time.timeSinceLevelLoad;
    }

    public RaycastHit? HitForCollider(Collider otherCollider)
    {
        return hitInfo.HasValue && 
                      hitInfo.Value.collider == otherCollider ? 
                      hitInfo :
                      null;
    }
}
