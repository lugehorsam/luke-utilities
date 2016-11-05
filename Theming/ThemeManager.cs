using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class ThemeManager<TTheme> : ScriptableObject
    where TTheme : Theme 
{
    TTheme CurrentBundle {
        get {
            return themeBundles [currentThemeIndex];
        }
    }

    [SerializeField]
    TTheme[] themeBundles;

    int currentThemeIndex;

    HashSet<IThemeable<TTheme>> themeables = new HashSet<IThemeable<TTheme>>();

    public void SetThemeIndex (int index)
    {
        currentThemeIndex = index;
        ApplyCurrentTheme ();
    }
     
    public void RegisterThemeable(IThemeable<TTheme> themeable)
    {
        if (!themeables.Add (themeable)) {
            Diagnostics.Report ("trying to add a duplicate theamble");
        } else {
            themeable.HandleNewTheme (CurrentBundle);
        }
    }

    public void DeregisterThemeable(IThemeable<TTheme> themeable)
    {
        if (!themeables.Remove (themeable)) {
            Diagnostics.Report ("trying to remove a nonexistant theamble");
        }
    }

    void ApplyCurrentTheme ()
    {
        foreach (IThemeable<TTheme> themeable in themeables) {
            themeable.HandleNewTheme (CurrentBundle);
        }
    }
}
