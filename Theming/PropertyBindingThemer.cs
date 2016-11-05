using UnityEngine;
using System;

public abstract class PropertyBindingThemer<TThemeManager, TTheme, TBinding, TProperty, TComponent> 
    : MonoBehaviour, IThemeable<TTheme>
    where TThemeManager : ThemeManager<TTheme>
    where TTheme : Theme
    where TBinding : PropertyBinding<TProperty, TComponent>
    where TComponent : Component
{

    [SerializeField]
    TThemeManager themeManager;

    TBinding binding;

    void Awake ()
    {
        binding = GetComponent<TBinding> ();
        themeManager.RegisterThemeable (this);
    }

    void IThemeable<TTheme>.HandleNewTheme(TTheme theme) {
        TProperty themeProp = GetThemeProperty (theme);
        binding.SetProperty (themeProp);
    }

    protected abstract TProperty GetThemeProperty (TTheme theme);

    void OnDestroy ()
    {
        themeManager.DeregisterThemeable (this);
    }
}
