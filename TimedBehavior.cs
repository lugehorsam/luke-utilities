using UnityEngine;
using System;

public class TimedBehavior : MonoBehaviour {

    public event Action<TimedBehavior> OnExpire = (obj) => { };

    [SerializeField] 
    float startLifeTime;
    float lifeTime; //in seconds

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            OnExpire(this);
        }
    }

    public void ResetTimer()
    {
        lifeTime = startLifeTime;
    }
}
