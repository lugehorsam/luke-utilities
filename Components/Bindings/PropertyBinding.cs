using UnityEngine;
using System;

[Serializable]
public abstract class PropertyBinding<TProperty, TComponent>
    where TComponent : Component {

    protected TComponent Component { get; private set; }
    protected GameObject GameObject { get; private set; }


    protected PropertyBinding(GameObject gameObject)
    {
        Component = gameObject.AddComponent<TComponent>();
        GameObject = gameObject;
    }
    
    public abstract TProperty GetProperty();
    public abstract void SetProperty(TProperty property);

}
