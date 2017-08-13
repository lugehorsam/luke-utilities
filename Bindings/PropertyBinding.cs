using UnityEngine;
using System;

[Serializable]
public abstract class PropertyBinding<TProperty, TComponent>
    where TComponent : Component {

    protected TComponent Component { get; }

    protected PropertyBinding(TComponent component)
    {
        Component = component;
    }
    
    public abstract TProperty GetProperty();
    public abstract void SetProperty(TProperty property);

}
