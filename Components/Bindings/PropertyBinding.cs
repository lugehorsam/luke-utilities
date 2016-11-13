using UnityEngine;
using System.Collections;
using System;

[Serializable]
public abstract class PropertyBinding<TProperty, TComponent> : GameBehavior
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
