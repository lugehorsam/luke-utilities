using UnityEngine;
using System.Collections.Generic;

namespace Theming
{

    public abstract class ThemeManager<TTheme> : ScriptableObject
        where TTheme : ScriptableObject
    {
        TTheme CurrentTheme {
            get {
                return themes [currentThemeIndex];
            }
        }

        [SerializeField]
        TTheme[] themes;

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
                themeable.HandleNewTheme (CurrentTheme);
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
                themeable.HandleNewTheme (CurrentTheme);
            }
        }
    }


}