using UnityEngine;

public struct GestureFrame {

    /// <summary>
    /// Position of the gesture frame in screen space.
    /// </summary>
    public Vector2 Position
    {
        get
        {
            return _position;
        }
    }
    
    private readonly Vector2 _position;

    RaycastHit? _hitInfo;

    public float Time
    {
        get
        {
            return _time;
        }
    }
    
    float _time;

    public GestureFrame(Vector2 position, RaycastHit? hitInfo = null)
    {
        _position = position;
        _hitInfo = hitInfo;
        _time = UnityEngine.Time.timeSinceLevelLoad;
    }

    public RaycastHit? HitForCollider(Collider otherCollider)
    {
        return _hitInfo.HasValue && 
                      _hitInfo.Value.collider == otherCollider ? 
                      _hitInfo :
                      null;
    }
    
    public RaycastHit? HitForCollider(Collider2D otherCollider)
    {
        return _hitInfo.HasValue && 
               _hitInfo.Value.collider == otherCollider ? 
            _hitInfo :
            null;
    }

    public override string ToString()
    {
        return string.Format("[GestureFrame: Position={0}, Time={1}, HitInfo={2}]", Position, Time, _hitInfo);
    }
}
