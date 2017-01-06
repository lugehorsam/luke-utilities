using UnityEngine;
using System;

[Serializable]
public class LazyPrefab
{
    [SerializeField]
    private Prefab prefab;

    private GameObject instance;

    public T GetInstance<T>() where T : Component
    {
        instance = instance ?? prefab.Instantiate();
        return instance.GetComponent<T>();
    }
}
