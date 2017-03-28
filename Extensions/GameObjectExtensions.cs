using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameObjectExtensions
{

    public static void SetMeshColor (this GameObject thisObject, Color color)
    {
        thisObject.GetComponent<MeshRenderer> ().material.color = color;
    }

    public static T GetComponentWithInterface<T>(this GameObject thisObject) where T : class {
        Component[] components = thisObject.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++) {
            if (typeof(T).IsAssignableFrom(components[i].GetType())) {
                return components[i] as T;
            }                
        }
        return null;
    }

    public static T[] GetComponentsWithInterface<T> (this GameObject thisObject) where T : class
    {
        List<T> interfaces = new List<T> ();
        Component [] components = thisObject.GetComponents<Component> ();
        for (int i = 0; i < components.Length; i++)
        {
            List<Type> potentialImplementingTypes = new List<Type>();
            Type componentType = components[i].GetType();
            potentialImplementingTypes.Add(componentType);
            IEnumerable<Type> parentTypes = componentType.GetParentTypes();
            potentialImplementingTypes.AddRange(parentTypes);

            foreach (var type in potentialImplementingTypes)
            {
                if (typeof (T).IsAssignableFrom (type))
                {
                    interfaces.Add (components [i] as T);
                    break;
                }
            }
        }
        return interfaces.ToArray();
    }

    public static T [] GetComponentsOfGenericType<T> (this GameObject thisObject) where T : class
    {
        Component [] components = thisObject.GetComponents<Component> ();
        List<T> componentsOfType = new List<T> ();

        foreach (Component component in components) {
            if (ReflectionUtils.IsAssignableToGenericType (component.GetType (), typeof(T))) {
                componentsOfType.Add (component as T);
            }
        }
        return componentsOfType.ToArray();
    }

    public static T GetOrAddComponent<T>(this GameObject thisGameObject) where T : Component
    {
        T component = thisGameObject.GetComponent<T>();
        
        if (component == null)
            component = thisGameObject.AddComponent<T>();
        
        Diagnostics.Log("returning " + component);
        return component;
    }
}
