using UnityEngine;
using System;

[Serializable]
public abstract class PropertyBinding<TProperty, TComponent> : MonoBehaviour
    where TComponent : Component {

    public TComponent Component {
        get {
            return component;
        }
    }

    [SerializeField]
    TComponent component;

    public abstract TProperty GetProperty();
    public abstract void SetProperty(TProperty property);

}
