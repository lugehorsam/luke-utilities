using UnityEngine;
using System;

[Serializable]
public abstract class PropertyBinding<TProperty, TComponent>
    where TComponent : Component {

    protected TComponent Component { get; }
    protected GameObject GameObject { get; }


    protected PropertyBinding(GameObject gameObject, TComponent component)
    {
        Component = component;
        GameObject = gameObject;
    }
    
    public abstract TProperty GetProperty();
    public abstract void SetProperty(TProperty property);

}
